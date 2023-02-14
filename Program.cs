using MarsUndiscoveredApi;
using MarsUndiscoveredApi.Models;
using MarsUndiscoveredApi.Services;
using MarsUndiscoveredApi.Extensions;

using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();
    
    var telemetryConnectionString = Environment.GetEnvironmentVariable(Constants.MarsUndiscoveredTelemetryConnectionString);

    if (!String.IsNullOrEmpty(telemetryConnectionString))
        builder.Services.AddApplicationInsightsTelemetry(c => c.ConnectionString = telemetryConnectionString);
    else
        builder.Services.AddApplicationInsightsTelemetry();

    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger();

    builder.Services.AddSingleton(Log.Logger);

    // Add services to the container.

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var databaseSettings = new DatabaseSettings();

    var databaseFileConfiguration = builder.Configuration.GetSection("MarsUndiscoveredDatabase");

    databaseSettings.ConnectionString = databaseFileConfiguration.GetValueOrEnvironmentVariable(Constants.MarsUndiscoveredDbConnectionString);
    databaseSettings.DatabaseName = databaseFileConfiguration.GetValueOrEnvironmentVariable(Constants.MarsUndiscoveredDbName);
    databaseSettings.MorgueCollectionName = databaseFileConfiguration.GetValueOrEnvironmentVariable(Constants.MarsUndiscoveredMorgueCollectionName);
    
    builder.Services.AddSingleton(databaseSettings);
    
    builder.Services.AddSingleton<IMorgueService, MorgueService>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.MapControllers();
    app.MapFallbackToFile("index.html");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application startup failed");
}
finally
{
    Log.CloseAndFlush();
}
