using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.Users;
using BeautySalon.Services.Users.Contracts.Dtos;

namespace BeautySalon.Services.Users.Contracts;
public interface IUserRepository : IRepository
{
    Task Add(User user);
    Task<GetUserForLoginDto?> GetByUserIdForRefreshToken(string userId);
    Task<GetUserForLoginDto?> GetByUserNameForLogin(string userName);
    Task<bool> IsExistByMobileNumber(string mobile);
    Task<bool> IsExistByUserName(string userName);
}
