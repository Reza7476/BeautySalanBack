using BeautySalon.Common.Dtos;
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

    public async Task<GetSMSumberCreditDto?> GetSMSCountCredit()
    {
        var key = _setting.SMSSettings.SMSKey;
        Uri apiBaseAddress = new Uri("https://console.melipayamak.com");
        using (HttpClient client = new HttpClient() { BaseAddress = apiBaseAddress })
        {
            var result = client.GetAsync($"api/receive/credit/{key}").Result;
            var response = result.Content.ReadAsStringAsync().Result;
            if (response != "")
            {
                var creditResponse = JsonSerializer.Deserialize<GetSMSumberCreditDto>(response, new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true,
                });
                return creditResponse;
            }
            return null;
        }
    }

    public async Task<SendSMSResponseDto?> SendSMS(SendSMSDto dto)
    {
        var a = GetSMSBodyId(dto.BodyName);
        var key = _setting.SMSSettings.SMSKey;
        var payLoad = new
        {
            bodyId = a,
            to = dto.Number,
            args = dto.Args
        };
        Uri apiBaseAddress = new Uri("https://console.melipayamak.com");
        using (HttpClient client = new HttpClient() { BaseAddress = apiBaseAddress })
        {
            // You may need to Install-Package Microsoft.AspNet.WebApi.Client
            var result = client.PostAsJsonAsync($"api/send/shared/{key}", payLoad).Result;
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

    public async Task<VerifySMSDto?> VerifySMS(long recIds)
    {
        var key = _setting.SMSSettings.SMSKey;
        var payload = new
        {
            recIds = new[] { recIds }
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

    public Task<VerifySMSDto?> VerifySMS22(List<long> recIds)
    {
        throw new NotImplementedException();
    }

    private int GetSMSBodyId(string templateName)
    {
        int a = 1;
        switch (templateName)
        {
            case "OtpBodyIdShared":
                a = _setting.SMSSettings.OtpBodyIdShared;
                break;
            case "RegisterClient":
                a = _setting.SMSSettings.RegisterClient;
                break;

            //case "RemindingAppointment":
            //    a = _setting.SMSSettings.;
            default:
                a = 0;
                break;
        }

        return a;
    }
}
