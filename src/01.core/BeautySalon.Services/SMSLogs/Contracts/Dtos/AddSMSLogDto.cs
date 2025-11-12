using BeautySalon.Entities.SMSLogs;

namespace BeautySalon.Services.SMSLogs.Contracts.Dtos;
public class AddSMSLogDto
{
    public string Title { get; set; } = default!;
    public string ReceiverNumber { get; set; } = default!;
    public string Content { get; set; } = default!;
    public string? ResponseContent { get; set; }
    public long  RecId { get; set; }
    public SendSMSStatus Status { get; set; }
}
