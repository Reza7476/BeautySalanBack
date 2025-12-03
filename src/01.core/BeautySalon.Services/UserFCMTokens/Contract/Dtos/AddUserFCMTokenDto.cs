namespace BeautySalon.Services.UserFCMTokens.Contract.Dtos;
public class AddUserFCMTokenDto
{
    public string FCMToken { get; set; } = default!;
    public string? DeviceInfo { get; set; }
}
