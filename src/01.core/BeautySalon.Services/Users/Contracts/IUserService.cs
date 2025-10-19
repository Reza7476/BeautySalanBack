using BeautySalon.Common.Interfaces;
using BeautySalon.Services.Users.Contracts.Dtos;
using BeautySalon.Services.Users.Dtos;

namespace BeautySalon.Services.Users.Contracts;
public interface IUserService : IService
{
    Task<string> Add(AddUserDto dto);
    Task<GetUserForLoginDto?> GetByUserIdForRefreshToken(string userId);
    Task<GetUserForLoginDto?> GetByUserNameForLogin(string userName);
    Task<bool> IsExistByMobileNumber(string mobileNumber);
    Task<bool> IsExistByUserName(string userName);
}
