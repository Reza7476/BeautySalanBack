namespace BeautySalon.Entities.SMSLogs;
public class SMSLog
{
    public string Id { get; set; } = default!;
    public long RecId { get; set; }
    public string ReceiverNumber { get; set; } = default!;
    public string Message { get; set; } = default!;
    public SendSMSStatus Status { get; set; }
    public string? ErrorMessage { get; set; } 
    public DateTime CreatedAt { get; set; }
    public string ProviderNumber { get; set; } = default!;
}


public  enum SendSMSStatus
{
    Pending=1,
    Sent=2,
    Failed=3,
    Delivered=4
}