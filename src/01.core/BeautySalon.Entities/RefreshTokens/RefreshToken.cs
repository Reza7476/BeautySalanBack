using BeautySalon.Entities.Users;

namespace BeautySalon.Entities.RefreshTokens;
public class RefreshToken
{
    public long Id { get; set; }
    public string Token { get; set; } = default!;
    public DateTime ExpireAt { get; set; }
    public bool IsRevoked { get; set; }
    public string UserId { get; set; } = default!;
    public User User { get; set; } = default!;
}
