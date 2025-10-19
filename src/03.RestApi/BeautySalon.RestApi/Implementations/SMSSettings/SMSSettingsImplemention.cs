using BeautySalon.Common.Dtos;
using BeautySalon.Common.Interfaces;

namespace BeautySalon.RestApi.Configurations.SMS;

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
            CreditUrl = config["SMSSettings:CreditUrl"]!,
            ProviderNumber = config["SMSSettings:ProviderNumber"]!,
            ReceiveUrl = config["SMSSettings:ReceiveUrl"]!,
            SendApiUrl = config["SMSSettings:SendApiUrl"]!,
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
            CreditUrl = config["SMSSettings:CreditUrl"]!,
            ProviderNumber = config["SMSSettings:ProviderNumber"]!,
            ReceiveUrl = config["SMSSettings:ReceiveUrl"]!,
            SendApiUrl = config["SMSSettings:SendApiUrl"]!,
            SMSKey = config["SMSSettings:SMSKey"]!
        };

    }
}