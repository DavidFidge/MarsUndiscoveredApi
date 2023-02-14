namespace MarsUndiscoveredApi.Extensions
{
    public static class ConfigurationExtensions
    {
        public static string GetValueOrEnvironmentVariable(this IConfiguration? configuration, string variableName)
        {
            var value = configuration?.GetValue<string?>(variableName, null) ?? null;

            if (String.IsNullOrEmpty(value))
                value = Environment.GetEnvironmentVariable(variableName);

            if (String.IsNullOrEmpty(value))
                throw new Exception(
                    $"Could not find an appsettings.json configuration or an environment variable with name {variableName}");

            return value;
        }
    }
}