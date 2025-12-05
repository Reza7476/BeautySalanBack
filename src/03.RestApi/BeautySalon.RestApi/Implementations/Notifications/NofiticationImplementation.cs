using BeautySalon.Common.Interfaces;
using BeautySalon.Services;
using BeautySalon.Services.UserFCMTokens.Contract;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace BeautySalon.RestApi.Implementations.Notifications;

public class NofiticationImplementation : INotificationJobs
{
    private readonly IUserFCMTokenService _userFcmTokenService;
    private readonly IFireBaseAuthService _firebaseAuthService;
    private readonly IHttpClientFactory _httpClientFactory;
    public NofiticationImplementation(IUserFCMTokenService userFcmTokenService,
        IFireBaseAuthService firebaseAuthService,
        IHttpClientFactory httpClientFactory)
    {
        _userFcmTokenService = userFcmTokenService;
        _firebaseAuthService = firebaseAuthService;
        _httpClientFactory = httpClientFactory;
    }

    public async Task SendNewAppointmentNotification(string appointmentId)
    {

        var fcmTokens = await _userFcmTokenService.GetReciviersFCMToken(SystemRole.Admin);


        var accessToken = await _firebaseAuthService.GetAccessTokenAsync();
        var client = _httpClientFactory.CreateClient("fcm");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        foreach (var fcmToken in fcmTokens)
        {
            var payload = new
            {
                message = new
                {
                    token = fcmToken.Token,
                    notification = new { title = "نوبت جدید", body = "یک نوبت جدید ثبت شد" },
                    data = new { receiver = SystemRole.Admin, type = "NewAppointment", appointmentId }
                }
            };

            var json = JsonSerializer.Serialize(payload);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://fcm.googleapis.com/v1/projects/beautysalon-f3295/messages:send", content);

            if (!response.IsSuccessStatusCode)
            {
                await _userFcmTokenService.RemoveToken(fcmToken.Id);
            }
        }
    }
}
