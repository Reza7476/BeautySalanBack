using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.Users;
using BeautySalon.Services.UserFCMTokens.Contract.Dtos;

namespace BeautySalon.Services.UserFCMTokens.Contract;
public interface IUserFCMTokenRepository : IRepository
{
    Task Add(UserFCMToken userFCMToken);
    Task<List<GetFCMTokenForSendNotificationDto>> GetAdminsToken();
    Task<bool> IsExistByFCMTokenAndUserIdAndIsActive(string fCMToken, string userId);
}
