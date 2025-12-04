using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.Users;
using BeautySalon.Services.UserFCMTokens.Contract.Dtos;

namespace BeautySalon.Services.UserFCMTokens.Contract;
public interface IUserFCMTokenRepository : IRepository
{
    Task Add(UserFCMToken userFCMToken);
    Task AddRange(List<UserFCMToken> userFCMTokens);
    Task<List<GetFCMTokenForSendNotificationDto>> GetAdminsToken();
    Task<List<string>> GetUserRolesByUserId(string userId);
    Task<bool> IsExistByFCMTokenAndUserIdAndIsActiveAndRole(string fCMToken, string userId, string role);
}
