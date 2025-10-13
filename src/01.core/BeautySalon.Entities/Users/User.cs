using BeautySalon.Entities.Commons;

namespace BeautySalon.Entities.Users;
public class User
{
    public string Id { get; set; } = default!;
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Mobile { get; set; }
    public string? UserName { get; set; }
    public string? HashPass { get; set; }
    public string? Email { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? BirthDate { get; set; }
    public bool IsActive { get; set; }
    public MediaDocument? Avatar { get; set; }
    public List<UserRole> Roles { get; set; } = default!;

}
