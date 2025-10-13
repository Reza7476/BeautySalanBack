using BeautySalon.Entities.Users;

namespace BeautySalon.Entities.Roles;
public class Role
{
    public Role()
    {
        UserRoles = new List<UserRole>();
    }
    public long Id { get; set; }
    public string RoleName { get; set; } = default!;
    public DateTime CreationDate { get; set; }
    public List<UserRole> UserRoles { get; set; }
}
