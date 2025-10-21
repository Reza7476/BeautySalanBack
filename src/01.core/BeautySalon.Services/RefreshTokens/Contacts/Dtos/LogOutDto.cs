using System.ComponentModel.DataAnnotations;

namespace BeautySalon.Services.RefreshTokens.Contacts.Dtos;
public class LogOutDto
{
    [Required]
    public string RefreshToken { get; set; } = default!;
}
