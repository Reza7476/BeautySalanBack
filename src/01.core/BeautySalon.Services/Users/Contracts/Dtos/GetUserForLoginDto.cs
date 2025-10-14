using System.Security.Principal;

namespace BeautySalon.Services.Users.Contracts.Dtos;
public class GetUserForLoginDto
{
    public string? UserName { get; set; }
    public string? HashPass { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string?  Id { get; set; }
    public string? Mobile { get; set; }
    public string? Email { get; set; }
    public List<string> UserRoles { get; set; } = new();
}
