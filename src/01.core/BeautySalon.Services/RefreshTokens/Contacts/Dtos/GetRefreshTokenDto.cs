namespace BeautySalon.Services.RefreshTokens.Contacts.Dtos;
public class GetRefreshTokenDto
{
    public string Token { get; set; } = default!;
    public string UserId { get; set; } = default!;
    public DateTime ExpireAt { get; set; }
    public bool IsRevoked { get; set; }
}
