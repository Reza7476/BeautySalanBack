using BeautySalon.Entities.Users;
using BeautySalon.Services.OTPRequests.Contacts;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.infrastructure.Persistence.OtpRequests;
public class EFOtpRequestRepository : IOtpRequestRepository
{

    private readonly DbSet<OtpRequest> _otpRequests;

    public EFOtpRequestRepository(EFDataContext context)
    {
        _otpRequests = context.Set<OtpRequest>();
    }
    public async Task Add(OtpRequest otpRequest)
    {
        await _otpRequests.AddAsync(otpRequest);
    }
}
