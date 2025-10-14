namespace BeautySalon.Common.Dtos;
public class JwtSettingDto
{
    public string Key { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int AccessTokenExpirationMinutes { get; set; } = 59;
    public int RefreshTokenExpirationDays { get; set; } = 7;
}
