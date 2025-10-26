using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.OTPRequests;
using BeautySalon.Services.OTPRequests.Contracts.Dtos;

namespace BeautySalon.Services.OTPRequests.Contracts;
public interface IOtpRequestRepository : IRepository
{
    Task Add(OtpRequest otpRequest);
    Task<OtpRequest?> FindById(string id);
    Task<GetOtpRequestForRegisterDto?> GetByIdForRegister(string id);
    Task<GetOtpRequestForRegisterDto?> GetByIdForResetPassword(string id);
}
