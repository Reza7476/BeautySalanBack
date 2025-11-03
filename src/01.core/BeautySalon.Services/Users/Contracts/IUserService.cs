using BeautySalon.Common.Dtos;
using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.Users;
using BeautySalon.Services.Users.Contracts.Dtos;

namespace BeautySalon.Services.Users.Contracts;
public interface IUserService : IService
{
    Task<string> Add(AddUserDto dto);
    Task ChangePassword(string newPassword, string mobile);
    Task EditProfile(EditUserProfileDto dto, string? id);
    Task<GetUserForLoginDto?> GetByUserIdForRefreshToken(string userId);
    Task<GetUserForLoginDto?> GetByUserNameForLogin(string userName);
    Task <User?>GetUserForEditProfileImage(string userId);
    Task<string?> GetUserIdByMobileNumber(string mobileNumber);
    Task<GetUserInfoDto?> GetUserInfo(string? userId);
    Task<bool> IsExistByMobileNumber(string mobileNumber);
    Task<bool> IsExistByUserName(string userName);
    Task<bool> IsExistByUserNameExceptItSelf(string id, string userName);
}
