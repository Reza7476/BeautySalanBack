using BeautySalon.Entities.Roles;

namespace BeautySalon.Entities.Users;
public class UserRole
{
    public long Id { get; set; }
    public long RoleId { get; set; }
    public string UserId { get; set; } = default!;
    public User User { get; set; } = default!;
    public Role  Role { get; set; }=default!;
}
