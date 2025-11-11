using BeautySalon.Common.Dtos;

namespace BeautySalon.Services.Users.Contracts.Dtos;
public class GetAllUsersDto
{
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Mobile { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public List<string> Roles { get; set; } = new();
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public int AppointmentNumber { get; set; }
    public ImageDetailsDto? Avatar { get; set; }
    public string Id { get; set; } = default!;
}
