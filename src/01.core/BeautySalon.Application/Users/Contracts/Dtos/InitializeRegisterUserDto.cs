using System.ComponentModel.DataAnnotations;

namespace BeautySalon.Application.Users.Contracts.Dtos;
public class InitializeRegisterUserDto
{
    [Required]
    public string MobileNumber { get; set; } = default!;

}
