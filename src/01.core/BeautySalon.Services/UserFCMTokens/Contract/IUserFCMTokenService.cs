using BeautySalon.Common.Interfaces;
using BeautySalon.Services.UserFCMTokens.Contract.Dtos;

namespace BeautySalon.Services.UserFCMTokens.Contract;
public interface IUserFCMTokenService : IService
{
    Task Add(AddUserFCMTokenDto dto, string userId);
    Task<List<GetFCMTokenForSendNotificationDto>> GetReciviersFCMToken(string role);
}
