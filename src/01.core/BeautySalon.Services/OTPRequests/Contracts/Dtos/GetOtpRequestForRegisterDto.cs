using BeautySalon.Entities.OTPRequests;

namespace BeautySalon.Services.OTPRequests.Contracts.Dtos;
public class GetOtpRequestForRegisterDto
{
    public string Mobile { get; set; } = default!;
    public string OtpCode { get; set; } = default!;//one-time-password
    public OtpPurpose Purpose { get; set; }
    public DateTime ExpireAt { get; set; }
    public DateTime CreatedAt { get; set; }
}
