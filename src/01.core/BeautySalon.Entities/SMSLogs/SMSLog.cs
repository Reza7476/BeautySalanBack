namespace BeautySalon.Entities.SMSLogs;
public class SMSLog
{
    public string Id { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
    public string ReceiverNumber { get; set; } = default!;
    public long RecId { get; set; }
    public SendSMSStatus Status { get; set; }
    public string? ResponseContent { get; set; }
    public DateTime CreatedAt { get; set; }
}


public enum SendSMSStatus : byte
{
    NotResponse=0,
    Pending = 1,
    Sent = 2,
    Failed = 3,
    Delivered = 4
}