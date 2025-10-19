using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.Users;
using BeautySalon.Services.OTPRequests.Contacts.Dtos;

namespace BeautySalon.Services.OTPRequests.Contacts;
public interface IOtpRequestRepository : IRepository
{
    Task Add(OtpRequest otpRequest);
    Task<OtpRequest?> FindById(string id);
    Task<GetOtpRequestForRegisterDto?> GetByIdForRegister(string id);
}
