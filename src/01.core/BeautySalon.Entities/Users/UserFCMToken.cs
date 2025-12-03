namespace BeautySalon.Entities.Users;
public class UserFCMToken
{
    public string Id { get; set; } = default!;
    public string UserId { get; set; } = default!;
    public string FCMToken { get; set; } = default!;
    public string? DeviceInfo { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
    public User User { get; set; } = default!;
}

