using BeautySalon.Common.Dtos;
using BeautySalon.Common.Interfaces;

namespace BeautySalon.RestApi.Implementations.GoogleCeridentials;

public class GoogleCredentialRootPathImplemention : IGoogleCredentialRootPath
{
    public GoogleCredentialDto GoogleCredentialPath { get; private set; }

    public GoogleCredentialRootPathImplemention(string environment,string contentRootPath)
    {
        GoogleCredentialPath = environment.Equals("Development", StringComparison.OrdinalIgnoreCase)
            ? LoadFromEnvironmentVariable(contentRootPath)
            : LoadFromSecret(contentRootPath);
    }


    private GoogleCredentialDto LoadFromEnvironmentVariable(string contentRootPath)
    {
        var config = new ConfigurationBuilder()
             .SetBasePath(contentRootPath)
             .AddJsonFile("appsettings.Production.json", optional: false, reloadOnChange: true)
             .Build();

        return new GoogleCredentialDto
        {
            Root=Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS")
        };
    }


    private GoogleCredentialDto LoadFromSecret(string contentRootPath)
    {
        var secretPath = Path.Combine(contentRootPath, "GoogleSecurity", "beautysalon-f3295-firebase-adminsdk-fbsvc-42695222a1.json");
        return new GoogleCredentialDto
        {
            Root = secretPath
        };
    }
}

