using BeautySalon.Common.Interfaces;
using BeautySalon.Services.Users.Dtos;

namespace BeautySalon.Services.Users.Contracts;
public interface IUserService : IService
{
    Task<string> Add(AddUserDto dto);
    Task<bool> IsExistByUserName(string userName);
}
