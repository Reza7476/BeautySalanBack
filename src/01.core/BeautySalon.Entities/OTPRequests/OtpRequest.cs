namespace BeautySalon.Entities.Users;
public class OtpRequest
{
    public string Id { get; set; } = default!;

    public string Mobile { get; set; } = default!;
    public string OtpCode { get; set; } = default!;//one-time-password
    public bool IsUsed { get; set; }
    public OtpPurpose Purpose { get; set; }
    public DateTime ExpireAt { get; set; }
    public DateTime CreatedAt { get; set; }

}


public enum OtpPurpose
{
    Register = 1,
    ResetPassword = 2,
}