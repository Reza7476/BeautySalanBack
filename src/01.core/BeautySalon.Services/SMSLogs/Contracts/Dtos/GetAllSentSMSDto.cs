using BeautySalon.Entities.SMSLogs;

namespace BeautySalon.Services.SMSLogs.Contracts.Dtos;
public class GetAllSentSMSDto
{
    public long RecId { get; set; }
    public string ReceiverNumber { get; set; } = default!;
    public string Message { get; set; } = default!;
    public SendSMSStatus Status { get; set; }
    public string? ErrorMessage { get; set; }
    public DateTime CreatedAt { get; set; }
    public string ProviderNumber { get; set; } = default!;
}
