using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.Roles;
using BeautySalon.Entities.Users;
using BeautySalon.Services.Roles.Contracts.Dtos;

namespace BeautySalon.Services.Roles.Contracts;
public interface IRoleRepository : IRepository
{
    Task Add(Role newRole);
    Task AssignRoleToUser(UserRole newUserRole);
    Task<GetRoleDto?> GetRoleByName(string roleName);
    Task<bool> IsExistByName(string roleName);
}
