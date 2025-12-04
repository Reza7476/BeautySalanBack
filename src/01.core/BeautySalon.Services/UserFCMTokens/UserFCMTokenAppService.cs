using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.Users;
using BeautySalon.Services.UserFCMTokens.Contract;
using BeautySalon.Services.UserFCMTokens.Contract.Dtos;

namespace BeautySalon.Services.UserFCMTokens;
public class UserFCMTokenAppService : IUserFCMTokenService
{

    private readonly IUserFCMTokenRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeService _dateTimeService;


    public UserFCMTokenAppService(
        IUserFCMTokenRepository repository,
        IUnitOfWork unitOfWork,
        IDateTimeService dateTimeService)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _dateTimeService = dateTimeService;
    }

    public async Task Add(AddUserFCMTokenDto dto, string userId)
    {
        var userRoles = await _repository.GetUserRolesByUserId(userId);

        List<UserFCMToken> userFCMTokens = new List<UserFCMToken>();

        if (userRoles.Any())
        {
            foreach (var item in userRoles)
            {
                var oldToken = await _repository.IsExistByFCMTokenAndUserIdAndIsActiveAndRole(dto.FCMToken, userId, item);

                if (!oldToken)
                {
                    userFCMTokens.Add(new UserFCMToken()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Role = item,
                        CreatedAt = _dateTimeService.Now,
                        FCMToken = dto.FCMToken,
                        IsActive = true,
                        UserId = userId
                    });
                }
            }
            if (userFCMTokens.Any())
            {
                await _repository.AddRange(userFCMTokens);
            }
            await _unitOfWork.Complete();
        }

    }

    public async Task<List<GetFCMTokenForSendNotificationDto>> GetReciviersFCMToken(string role)
    {
        return await _repository.GetReciviersFCMToken(role);
    }

    public async Task RemoveToken(string fcmToken, string role)
    {
        var fcm = await _repository.FindByFCMToken(fcmToken,role);
        if (fcm != null)
        {
            await _repository.Remove(fcm);
            await _unitOfWork.Complete();   
        }

    }
}
