using BeautySalon.Entities.Roles;
using BeautySalon.Entities.Users;
using BeautySalon.Services.Roles.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.infrastructure.Persistence.Roles;
public class EFRoleRepository : IRoleRepository
{
    private readonly DbSet<Role> _roles;
    private readonly DbSet<UserRole> _userRoles;

    public EFRoleRepository(EFDataContext context)
    {
        _roles = context.Set<Role>();
        _userRoles = context.Set<UserRole>();

    }

    public async Task Add(Role newRole)
    {
        await _roles.AddAsync(newRole);
    }

    public async Task AssignRoleToUser(UserRole newUserRole)
    {
        await _userRoles.AddAsync(newUserRole);
    }

    public async Task<bool> IsExistByName(string roleName)
    {
        return await _roles.AnyAsync(_ => _.RoleName == roleName);
    }
}
