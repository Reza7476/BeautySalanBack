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

        var oldToken = await _repository.IsExistByFCMTokenAndUserIdAndIsActive(dto.FCMToken, userId);
        if (!oldToken)
        {
            var userFCMToken = new UserFCMToken()
            {
                Id = Guid.NewGuid().ToString(),
                DeviceInfo = dto.DeviceInfo,
                CreatedAt = _dateTimeService.Now,
                FCMToken = dto.FCMToken,
                IsActive = true,
                UserId = userId
            };

            await _repository.Add(userFCMToken);
            await _unitOfWork.Complete();
        }

    }
}
