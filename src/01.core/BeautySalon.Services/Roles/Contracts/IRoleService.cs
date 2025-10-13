using BeautySalon.Common.Interfaces;
using BeautySalon.Services.Roles.Contracts.Dtos;

namespace BeautySalon.Services.Roles.Contracts;
public interface IRoleService : IService
{
    Task<long> Add(AddRoleDto dto);
    Task AssignRoleToUser(string userId, long roleId);
}
