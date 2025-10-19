using BeautySalon.Entities.SMSLogs;

namespace BeautySalon.Services.SMSLogs.Contracts.Dtos;
public class AddSMSLogDto
{
    public string ReceiverNumber { get; set; } = default!;
    public string Message { get; set; } = default!;
    public string? ErrorMessage { get; set; }
    public string ProviderNumber { get; set; } = default!;
    public long  RecId { get; set; }
}
