using BeautySalon.Entities.OTPRequests;

namespace BeautySalon.Services.OTPRequests.Contracts.Dtos;
public class AddOTPRequestDto
{
    public string Mobile { get; set; } = default!;
    public string OtpCode { get; set; } = default!;//one-time-password
    public bool IsUsed { get; set; }
    public OtpPurpose Purpose { get; set; }
    public DateTime ExpireAt { get; set; }
}
