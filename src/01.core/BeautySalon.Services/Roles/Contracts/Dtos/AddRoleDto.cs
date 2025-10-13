using System.ComponentModel.DataAnnotations;

namespace BeautySalon.Services.Roles.Contracts.Dtos;
public class AddRoleDto
{
    [Required]
    public string RoleName { get; set; } = default!;
}
