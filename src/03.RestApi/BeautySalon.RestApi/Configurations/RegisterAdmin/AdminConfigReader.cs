namespace BeautySalon.RestApi.Configurations.RegisterAdmin;

public static class AdminConfigReader
{
    public static (string UserName, string Password) GetAdminCredential(string environmentName, string contentRootPath)
    {

        string userName = string.Empty;
        string password = string.Empty;
        if (environmentName.Equals("Development", StringComparison.OrdinalIgnoreCase))
        {
            userName = Environment.GetEnvironmentVariable("ADMIN_USERNAME")!;
            password = Environment.GetEnvironmentVariable("ADMIN_PASSWORD")!;
        }
        else if (environmentName.Equals("Production", StringComparison.OrdinalIgnoreCase))
        {
            var secretsPath = Path.Combine(contentRootPath, "Private", "Secrets.json");
            if (!File.Exists(secretsPath))
            {
                throw new FileNotFoundException($" Secrets file not found: ");
            }
            var config = new ConfigurationBuilder()
              .AddJsonFile(secretsPath, optional: false, reloadOnChange: false)
              .Build();
            userName = config["AdminConfig:UserName"]!;
            password = config["AdminConfig:Password"]!;
        }

        return (userName, password);

    }
}
