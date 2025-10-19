using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.Users;

namespace BeautySalon.Services.OTPRequests.Contacts;
public interface IOtpRequestRepository : IRepository
{
    Task Add(OtpRequest otpRequest);
}
