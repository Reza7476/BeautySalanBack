using BeautySalon.Common.Interfaces;
using Google.Apis.Auth.OAuth2;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace BeautySalon.RestApi.Implementations.FireBaseNotification;

public class FireBaseNotificationService : IFireBaseNotificationService
{
    private readonly IFireBaseSettingInfo _fireBaseSetting;
    private readonly HttpClient _httpClient;
    private readonly IGoogleCredentialRootPath _googleCredentialRootPath;

    public FireBaseNotificationService(
        IFireBaseSettingInfo fireBaseSetting,
        HttpClient httpClient,
        IGoogleCredentialRootPath googleCredentialRootPath)
    {
        _fireBaseSetting = fireBaseSetting;
        _httpClient = httpClient;
        _googleCredentialRootPath = googleCredentialRootPath;
    }

    public async Task<string> GetAccsessTokenAsync()
    {
        GoogleCredential credential;

        using (var stream = new FileStream(_googleCredentialRootPath.GoogleCredentialPath.Root!, FileMode.Open, FileAccess.Read))
        {
            credential = GoogleCredential.FromStream(stream)
                                         .CreateScoped("https://www.googleapis.com/auth/firebase.messaging");
        }

        var token = await credential.UnderlyingCredential.GetAccessTokenForRequestAsync();
        return token;
    }

    public async Task<bool> SendNotificationAsync(string fcmToken, string title, string body)
    {

        var token2 = await GetAccsessTokenAsync();


        var payload = new
        {
            message = new
            {
                token = fcmToken,
                notification = new
                {
                    title = title,
                    body = body
                }
            }
        };

        var jsonPayload = JsonSerializer.Serialize(payload);

        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token2);
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var a = "https://fcm.googleapis.com/v1/projects/beautysalon-f3295/messages:send";
        var response = await httpClient.PostAsync(a, new StringContent(jsonPayload, Encoding.UTF8, "application/json"));
        var responseContent = await response.Content.ReadAsStringAsync();
        return response.IsSuccessStatusCode;

    }
}

