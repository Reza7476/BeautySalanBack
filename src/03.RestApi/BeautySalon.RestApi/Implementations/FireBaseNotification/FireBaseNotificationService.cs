using BeautySalon.Common.Interfaces;
using Google.Apis.Auth.OAuth2;

namespace BeautySalon.RestApi.Implementations.FireBaseNotification;

public class FireBaseNotificationService : IFireBaseAuthService
{

    private readonly IGoogleCredentialRootPath _googleCredentialRootPath;

    public FireBaseNotificationService(
        IFireBaseSettingInfo fireBaseSetting,
        HttpClient httpClient,
        IGoogleCredentialRootPath googleCredentialRootPath)
    {
        _googleCredentialRootPath = googleCredentialRootPath;
    }

    public async Task<string> GetAccessTokenAsync()
    {
        GoogleCredential credential;

        using (var stream = new FileStream(
            _googleCredentialRootPath.GoogleCredentialPath.Root!,
            FileMode.Open,
            FileAccess.Read))
        {
            credential = GoogleCredential
                       .FromStream(stream)
                       .CreateScoped("https://www.googleapis.com/auth/firebase.messaging");
        }

        var token = await credential.UnderlyingCredential.GetAccessTokenForRequestAsync();
        return token;
    }
}

