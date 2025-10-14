namespace BeautySalon.Services.Users.Contracts.Dtos;
public class GetTokenDto
{
    public string JwtToken { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
}
