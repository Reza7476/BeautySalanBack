using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.Roles;
using BeautySalon.Entities.Users;

namespace BeautySalon.Services.Roles.Contracts;
public interface IRoleRepository : IRepository
{
    Task Add(Role newRole);
    Task AssignRoleToUser(UserRole newUserRole);
    Task<bool> IsExistByName(string roleName);
}
