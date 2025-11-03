using BeautySalon.Common.Interfaces;
using BeautySalon.Services.Users.Contracts.Dtos;

namespace BeautySalon.Services.Users.Contracts;
public interface IUserService : IService
{
    Task<string> Add(AddUserDto dto);
    Task ChangePassword(string newPassword, string mobile);
    Task<GetUserForLoginDto?> GetByUserIdForRefreshToken(string userId);
    Task<GetUserForLoginDto?> GetByUserNameForLogin(string userName);
    Task<string?> GetUserIdByMobileNumber(string mobileNumber);
    Task<GetUserInfoDto?> GetUserInfo(string? userId);
    Task<bool> IsExistByMobileNumber(string mobileNumber);
    Task<bool> IsExistByUserName(string userName);
}
