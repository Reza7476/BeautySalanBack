namespace BeautySalon.Common.Dtos;
public class SMSInformationDto
{
    public string ProviderNumber { get; set; } = default!;
    public string SMSKey { get; set; } = default!;
    public string BaseUrl { get; set; } = default!;
    public int OtpBodyIdShared { get; set; }
    public int RegisterClient { get; set; }
    public int RemindingAppointment { get; set; }
}
