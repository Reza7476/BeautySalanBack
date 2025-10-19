namespace BeautySalon.Common.Dtos;
public class SMSInformationDto
{
    public string SendApiUrl { get; set; } = default!;
    public string ReceiveUrl { get; set; } = default!;
    public string CreditUrl { get; set; } = default!;
    public string ProviderNumber { get; set; } = default!;
    public string SMSKey { get; set; } = default!;
}
