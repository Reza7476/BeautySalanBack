using BeautySalon.Common.Dtos;

namespace BeautySalon.RestApi.Configurations.JwtConfigs;

public static class JwtSettingLoader
{
    public static JwtSettingDto Load(string environment, string contentRootPaht)
    {
        var setting = new JwtSettingDto();

        IConfiguration config;
        if (environment.Equals("Development", StringComparison.OrdinalIgnoreCase))
        {
            var builder = new ConfigurationBuilder()
                .AddEnvironmentVariables();
            config = builder.Build();
            setting.Key = config["JWT_KEY"]!;
            setting.Issuer = config["JWT_ISSUER"]!;
            setting.Audience = config["JWT_AUDIENCE"]!;
        }
        else
        {
            var secretPath = Path.Combine(contentRootPaht, "Private", "Secrets.json");
            if (File.Exists(secretPath))
            {
                var builder = new ConfigurationBuilder()
                    .AddJsonFile(secretPath, optional: false, reloadOnChange: false);
                config = builder.Build();
                setting.Key = config["Jwt:Key"]!;
                setting.Issuer = config["Jwt:Issuer"]!;
                setting.Audience = config["Jwt:Audience"]!;
                setting.AccessTokenExpirationMinutes = int.Parse(config["Jwt:AccessTokenExpirationMinutes"]!);
                setting.RefreshTokenExpirationDays = int.Parse(config["Jwt:RefreshTokenExpirationDays"]!);
            }
        }
        return setting;
    }
}
