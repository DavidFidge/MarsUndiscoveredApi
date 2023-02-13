using MarsUndiscoveredApi.Models;
using MarsUndiscoveredApi.Services;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();
    
    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger();

    builder.Services.AddSingleton(Log.Logger);

    // Add services to the container.

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    
    builder.Services.Configure<DatabaseSettings>(
        builder.Configuration.GetSection("MarsUndiscoveredDatabase"));
    
    builder.Services.AddSingleton<IMorgueService, MorgueService>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseCors();
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
