using BeautySalon.Common.Interfaces;
using BeautySalon.Services.OTPRequests.Contacts.Dtos;

namespace BeautySalon.Services.OTPRequests.Contacts;
public interface IOtpRequestService : IService
{
    Task<string> Add(AddOTPRequestDto dto);
    Task ChangeIsUsedOtp(string otpRequestId);
    Task<GetOtpRequestForRegisterDto?> GetByIdForRegister(string id);
    Task<GetOtpRequestForRegisterDto?> GetByIdForResetPassword(string id);
}
