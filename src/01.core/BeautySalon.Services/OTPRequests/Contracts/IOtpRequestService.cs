using BeautySalon.Common.Interfaces;
using BeautySalon.Services.OTPRequests.Contracts.Dtos;

namespace BeautySalon.Services.OTPRequests.Contracts;
public interface IOtpRequestService : IService
{
    Task<string> Add(AddOTPRequestDto dto);
    Task ChangeIsUsedOtp(string otpRequestId);
    Task<GetOtpRequestForRegisterDto?> GetByIdForRegister(string id);
    Task<GetOtpRequestForRegisterDto?> GetByIdForResetPassword(string id);
}
