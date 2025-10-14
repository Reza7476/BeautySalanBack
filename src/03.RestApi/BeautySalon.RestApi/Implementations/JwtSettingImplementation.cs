using BeautySalon.Common.Dtos;
using BeautySalon.Common.Interfaces;

namespace BeautySalon.RestApi.Implementations;

public class JwtSettingImplementation : IJwtSettingService
{
    public JwtSettingDto JwtSetting { get; private set; }

    public JwtSettingImplementation(string environmentName, string contentRootPath)
    {
        JwtSetting = environmentName.Equals("Development", StringComparison.OrdinalIgnoreCase)
            ? LoadFromEnvironmentVariable()
            : LoadFromSecrets(contentRootPath);
    }


    private JwtSettingDto LoadFromEnvironmentVariable()
    {
        return new JwtSettingDto
        {
            Key = Environment.GetEnvironmentVariable("JWT_KEY")! ,
            Issuer = Environment.GetEnvironmentVariable("JWT_ISSUER") !,
            Audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE")!,
        };
    }

    private JwtSettingDto LoadFromSecrets(string contentRootPath)
    {
        var secretsPath = Path.Combine(contentRootPath, "Private", "Secrets.json");
        if (!File.Exists(secretsPath))
            throw new FileNotFoundException($"Secrets file not found at {secretsPath}");

        var config = new ConfigurationBuilder()
            .AddJsonFile(secretsPath, optional: false, reloadOnChange: false)
            .Build();

        return new JwtSettingDto
        {
            Key = config["JwtConfig:Key"]!,
            Issuer = config["JwtConfig:Issuer"]!,
            Audience = config["JwtConfig:Audience"]!,
            AccessTokenExpirationMinutes = int.Parse(config["JwtConfig:AccessTokenExpirationMinutes"]! ),
            RefreshTokenExpirationDays = int.Parse(config["JwtConfig:RefreshTokenExpirationDays"]!)
        };
    }
}
