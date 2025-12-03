using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.Users;

namespace BeautySalon.Services.UserFCMTokens.Contract;
public interface IUserFCMTokenRepository : IRepository
{
    Task Add(UserFCMToken userFCMToken);
    Task<bool> IsExistByFCMTokenAndUserIdAndIsActive(string fCMToken, string userId);
}
