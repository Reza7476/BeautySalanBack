using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.Roles;
using BeautySalon.Entities.Users;
using BeautySalon.Services.Roles.Contracts;
using BeautySalon.Services.Roles.Contracts.Dtos;
using BeautySalon.Services.Roles.Exceptions;

namespace BeautySalon.Services.Roles;
public class RoleAppService : IRoleService
{
    private readonly IRoleRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    public RoleAppService(IRoleRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<long> Add(AddRoleDto dto)
    {
        if(await _repository.IsExistByName(dto.RoleName))
        {
            throw new RoleNameIsDuplicateException();
        }

        var newRole = new Role()
        {
            CreationDate = DateTime.UtcNow,
            RoleName = dto.RoleName,
        };

        await _repository.Add(newRole);
        await _unitOfWork.Complete();
        return newRole.Id;
    }

    public  async Task AssignRoleToUser(string userId, long roleId)
    {


        var newUserRole = new UserRole()
        {
            RoleId = roleId,
            UserId = userId
        };
     
        await _repository.AssignRoleToUser(newUserRole);
        await _unitOfWork.Complete();

    }
}
