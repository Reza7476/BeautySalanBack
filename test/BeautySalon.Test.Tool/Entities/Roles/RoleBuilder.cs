using BeautySalon.Entities.Roles;

namespace BeautySalon.Test.Tool.Entities.Roles;
public class RoleBuilder
{
    private readonly Role _role;

    public RoleBuilder()
    {
        _role = new Role()
        {
            RoleName="role",
            
        };
    }


    public RoleBuilder WithName(string name)
    {
        _role.RoleName=name;
        return this;
    }


    public Role Build()
    {
        return _role;
    }
}
