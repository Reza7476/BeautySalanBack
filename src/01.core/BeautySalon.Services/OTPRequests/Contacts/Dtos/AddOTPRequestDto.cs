using BeautySalon.Entities.Users;

namespace BeautySalon.Services.OTPRequests.Contacts.Dtos;
public class AddOTPRequestDto
{
    public string Mobile { get; set; } = default!;
    public string OtpCode { get; set; } = default!;//one-time-password
    public bool IsUsed { get; set; }
    public OtpPurpose Purpose { get; set; }
    public DateTime ExpireAt { get; set; }
}
