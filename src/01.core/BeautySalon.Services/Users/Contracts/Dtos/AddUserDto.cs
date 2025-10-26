using System.ComponentModel.DataAnnotations;

namespace BeautySalon.Services.Users.Contracts.Dtos;
public class AddUserDto
{
    [Required]
    public string Name { get; set; } = default!;

    [Required]
    public string LastName { get; set; } = default!;

    [Required]
    public string Mobile { get; set; } = default!;

    [Required]
    public string UserName { get; set; } = default!;

    [Required]
    public string Password { get; set; } = default!;

    public string? Email { get; set; }
}
