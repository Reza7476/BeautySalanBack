using BeautySalon.Common.Dtos;

namespace BeautySalon.Services.Users.Contracts.Dtos;
public class GetUserInfoDto
{
    public string Id { get; set; } = default!;
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Mobile { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? BirthDate { get; set; }
    public bool IsActive { get; set; }
    public ImageDetailsDto? Avatar { get; set; }
    public List<String> RoleNames { get; set; } = default!;
}
