using BeautySalon.Common.Dtos;
using BeautySalon.Common.Interfaces;

namespace BeautySalon.RestApi.Implementations.FireBaseNotification;

public class FireBaseSettingInfoImplimention : IFireBaseSettingInfo
{
    public FireBaseSettingInfoDto FireBaseSettingInfo { get; private set; }



    public FireBaseSettingInfoImplimention(string environment, string contentRootPath)
    {
        FireBaseSettingInfo = environment.Equals("Development", StringComparison.OrdinalIgnoreCase)
            ? LoadFromEnvironmentVariable(contentRootPath)
            : LoadFromSecret(contentRootPath);
    }


    private FireBaseSettingInfoDto LoadFromEnvironmentVariable(string contentRootPath)
    {
        var config = new ConfigurationBuilder()
              .SetBasePath(contentRootPath)
              .AddJsonFile("appsettings.Production.json", optional: false, reloadOnChange: true)
              .Build();

        return new FireBaseSettingInfoDto
        {
            SenderId = Environment.GetEnvironmentVariable("FireBase_SenderId")!,
            ServerKey = Environment.GetEnvironmentVariable("FireBase_ServerKey")!
        };
    }


    private FireBaseSettingInfoDto LoadFromSecret(string contentRootPath)
    {
        var secretPath = Path.Combine(contentRootPath, "Private", "Secrets.json");

        var config = new ConfigurationBuilder()
            .AddJsonFile(secretPath, optional: false, reloadOnChange: true)
            .Build();
        return new FireBaseSettingInfoDto
        {
            ServerKey = config["FireBaseSetting:FireBase_ServerKey"]!,
            SenderId = config["FireBaseSetting:FireBase_SenderId"]!
        };
    }

}
