using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.Users;
using BeautySalon.Services.OTPRequests.Contacts;
using BeautySalon.Services.OTPRequests.Contacts.Dtos;

namespace BeautySalon.Services.OTPRequests;
public class OtpRequestAppService : IOtpRequestService
{

    private readonly IOtpRequestRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public OtpRequestAppService(
        IOtpRequestRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Add(AddOTPRequestDto dto)
    {
        var otpRequest = new OtpRequest()
        {
            CreatedAt = DateTime.UtcNow,
            ExpireAt = dto.ExpireAt,
            Id = Guid.NewGuid().ToString(),
            IsUsed = dto.IsUsed,
            Mobile = dto.Mobile,
            OtpCode = dto.OtpCode,
            Purpose = dto.Purpose,
        };
        await _repository.Add(otpRequest);
        await _unitOfWork.Complete();
        return otpRequest.Id;
    }

    public async Task ChangeIsUsedOtp(string id)
    {
        var otp = await _repository.FindById(id);
        if(otp != null) 
        {
            otp.IsUsed = true;
            await _unitOfWork.Complete();
        }

    }

    public async Task<GetOtpRequestForRegisterDto?> GetByIdForRegister(string id)
    {
        return await _repository.GetByIdForRegister(id);

    }
}
