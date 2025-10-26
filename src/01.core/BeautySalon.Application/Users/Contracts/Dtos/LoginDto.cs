using System.ComponentModel.DataAnnotations;

namespace BeautySalon.Application.Users.Contracts.Dtos;
public class LoginDto
{
    [Required]
    public string UserName { get; set; } = default!;

    [Required]
    public string Password { get; set; } = default!;
}
