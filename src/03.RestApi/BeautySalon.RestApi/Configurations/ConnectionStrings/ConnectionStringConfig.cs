namespace BeautySalon.RestApi.Configurations.ConnectionStrings;

public  static class ConnectionStringConfig
{
    public static (IConfiguration configuration, string connectionString ) LoadConfigAndConnectionString(string environmentName,string contentRootPath)
    {
        var builder = new ConfigurationBuilder()
              .SetBasePath(contentRootPath)
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
              .AddEnvironmentVariables();
      
        var privateConfigPath = Path.Combine(contentRootPath, "Private", "Secrets.json");

        if (File.Exists(privateConfigPath))
        {
            builder.AddJsonFile(privateConfigPath, optional: false, reloadOnChange: true);
            Console.WriteLine($"✅ Secrets file loaded from: {privateConfigPath}");
        }
        else
        {
            Console.WriteLine($"⚠️ Secrets file not found at: {privateConfigPath}");
        }

        var configuration = builder.Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

        connectionString = connectionString
                .Replace("${DB_USER}", configuration["DB_USER"] ?? Environment.GetEnvironmentVariable("DB_USER") ?? "")
                .Replace("${DB_PASS}", configuration["DB_PASS"] ?? Environment.GetEnvironmentVariable("DB_PASS") ?? "");

        return (configuration, connectionString);
    }
}
