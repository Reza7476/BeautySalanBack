using BeautySalon.Common.Dtos;
using BeautySalon.Common.Interfaces;

namespace BeautySalon.RestApi.Implementations.SMSSettings;

public class SMSSettingsImplementation : ISMSSetting
{
    public SMSInformationDto SMSSettings { get; private set; }

    public SMSSettingsImplementation(string environment, string contentRootPath)
    {
        SMSSettings = environment.Equals("Development", StringComparison.OrdinalIgnoreCase)
            ? LoadFromEnvironmentVariable(contentRootPath)
            : LoadFromSecret(contentRootPath);
    }

    private SMSInformationDto LoadFromEnvironmentVariable(string contentRootPath)
    {

        var config = new ConfigurationBuilder()
            .SetBasePath(contentRootPath)
            .AddJsonFile("appsettings.Production.json", optional: false, reloadOnChange: true)
            .Build();
        return new SMSInformationDto
        {
            SMSKey = Environment.GetEnvironmentVariable("SMSReza_Key")!,
            ProviderNumber = config["SMSSettings:ProviderNumber"]!,
            OtpBodyIdShared = int.Parse(config["SMSSettings:OtpBodyIdShared"]!),
            BaseUrl = config["SMSSettings:BaseUrl"]!
        };
    }


    private SMSInformationDto LoadFromSecret(string contentRootPath)
    {
        var secretPath = Path.Combine(contentRootPath, "Private", "Secrets.json");
        if (!File.Exists(secretPath))
        {
            throw new FileNotFoundException($"Secrets sms file not found.");
        }

        var config = new ConfigurationBuilder()
            .AddJsonFile(secretPath, optional: false, reloadOnChange: true)
            .Build();
        return new SMSInformationDto
        {
            ProviderNumber = config["SMSSettings:ProviderNumber"]!,
            SMSKey = config["SMSSettings:SMSKey"]!,
            BaseUrl = config["SMSSettings:BaseUrl"]!,
            OtpBodyIdShared = int.Parse(config["SMSSettings:OtpBodyIdShared"]!),
        };

    }
}