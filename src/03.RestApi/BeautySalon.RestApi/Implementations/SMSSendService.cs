using BeautySalon.Common.Dtos;
using BeautySalon.Common.Exceptions;
using BeautySalon.Common.Interfaces;
using System.Text.Json;

namespace BeautySalon.RestApi.Implementations;

public class SMSSendService : ISMSService
{

    private readonly ISMSSetting _setting;
    private readonly IHttpClientFactory _httpClient;

    public SMSSendService(ISMSSetting setting, IHttpClientFactory httpClient)
    {
        _setting = setting;
        _httpClient = httpClient;
    }

    public async Task<SendSMSResponseDto?> SendSMS(SendSMSDto dto)
    {
        var key = _setting.SMSSettings.SMSKey;
        var bodyId = _setting.SMSSettings.OtpBodyIdShared;
        var payLoad = new
        {
            bodyId = bodyId,
            to = dto.Number,
            args = new[] { dto.Message }
        };

        Uri apiBaseAddress = new Uri("https://console.melipayamak.com");
        using (HttpClient client = new HttpClient() { BaseAddress = apiBaseAddress })
        {
            // You may need to Install-Package Microsoft.AspNet.WebApi.Client
            var result = client.PostAsJsonAsync($"api/send/shared/{key}",
               payLoad).Result;
            var response = result.Content.ReadAsStringAsync().Result;
            if (response != "")
            {

                var smsResponse = JsonSerializer.Deserialize<SendSMSResponseDto>(response, new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });

                return smsResponse;
            }
            return null;
        }
    }

    public async Task<VerifySMSDto?> VerifySMS(long recId)
    {
        var key = _setting.SMSSettings.SMSKey;
        var payload = new
        {
            recIds = new[] { recId }
        };

        Uri apiBaseAddress = new Uri("https://console.melipayamak.com");
        using (HttpClient client = new HttpClient() { BaseAddress = apiBaseAddress })
        {
            // You may need to Install-Package Microsoft.AspNet.WebApi.Client
            var result = client.PostAsJsonAsync($"api/receive/status/{key}", payload).Result;
            var response = result.Content.ReadAsStringAsync().Result;

            if (response != "")
            {
                var smsStatus = JsonSerializer.Deserialize<VerifySMSDto>(response, new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });
                return smsStatus;
            }
            return null;
        }
    }
}
