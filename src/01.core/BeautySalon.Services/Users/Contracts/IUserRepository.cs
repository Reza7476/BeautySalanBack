using BeautySalon.Common.Dtos;
using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.Users;
using BeautySalon.Services.Users.Contracts.Dtos;

namespace BeautySalon.Services.Users.Contracts;
public interface IUserRepository : IRepository
{
    Task Add(User user);
    Task<User?> FindById(string id);
    Task<User?> FindByMobile(string mobile);
    Task<GetUserForLoginDto?> GetByUserIdForRefreshToken(string userId);
    Task<GetUserForLoginDto?> GetByUserNameForLogin(string userName);
    Task<string?> GetUserIdByMobileNumber(string mobileNumber);
    Task<GetUserInfoDto?> GetUserInfoById(string id);
    Task<bool> IsExistByMobileNumber(string mobile);
    Task<bool> IsExistByUserName(string userName);
    Task<bool> IsExistByUserNameExceptItSelf(string? userName, string id);
}
