namespace BeautySalon.Entities.Notifications;
public class Notification
{
    public string Id { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Body { get; set; } = default!;
    public string Receiver { get; set; } = default!;
    public string Type { get; set; } = default!;
    public string FCMToken { get; set; } = default!;
    public string UserId { get; set; } = default!;
    public bool IsSent { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime SentAt { get; set; }
}
