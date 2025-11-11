using BeautySalon.Entities.Users;

namespace BeautySalon.Test.Tool.Entities.Roles;
public class UserRoleBuilder
{
    private readonly UserRole _userRoles;

    public UserRoleBuilder()
    {
        _userRoles = new UserRole();
    }

    public UserRoleBuilder WithUser(string userId)
    {
        _userRoles.UserId = userId;
        return this;
    }

    public UserRoleBuilder WithRole(long roleId)
    {
        _userRoles.RoleId = roleId;
        return this;
    }

    public UserRole Build()
    {
        return _userRoles;
    }
}
