using BeautySalon.Common.Dtos;
using BeautySalon.Common.Exceptions;
using BeautySalon.Common.Interfaces;
using System.Text.Json;

namespace BeautySalon.RestApi.Implementations;

public class SMSSendService : ISMSService
{

    private readonly ISMSSetting _setting;
    private readonly HttpClient _httpClient;

    public SMSSendService(ISMSSetting setting, HttpClient httpClient)
    {
        _setting = setting;
        _httpClient = httpClient;
    }

    public async Task<SendSMSResponseDto> SendSMS(SendSMSDto dto)
    {
        var sendUrl = _setting.SMSSettings.SendApiUrl;
        var providerNumber = _setting.SMSSettings.ProviderNumber;
        var key = _setting.SMSSettings.SMSKey;
        var url = $"{sendUrl}/{key}";
        var payLoad = new
        {
            from = providerNumber,
            to = dto.Number,
            text = dto.Message
        };
        var response = _httpClient.PostAsJsonAsync(url, payLoad).Result;
        var raw = await response.Content.ReadAsStringAsync();
        var smsResponse = JsonSerializer.Deserialize<SendSMSResponseDto>(
            raw,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });

        return smsResponse!;
    }

    public async Task<VerifySMSDto> VerifySMS(long recId)
    {
        var receiveUrl = $"{_setting.SMSSettings.ReceiveUrl}/{_setting.SMSSettings.SMSKey}";
        var payload = new
        {
            recIds = new[] { recId }
        };

        var response = await _httpClient.PostAsJsonAsync(receiveUrl, payload);
        var raw = await response.Content.ReadAsStringAsync();
        var verifyResponse = JsonSerializer.Deserialize<VerifySMSDto>(
            raw,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });

        if (verifyResponse == null)
        {
            throw new VerifySMSException();
        }

        return verifyResponse;
    }
}
