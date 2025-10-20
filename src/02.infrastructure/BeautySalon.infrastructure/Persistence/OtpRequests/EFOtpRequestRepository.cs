using BeautySalon.Entities.Users;
using BeautySalon.Services.OTPRequests.Contacts;
using BeautySalon.Services.OTPRequests.Contacts.Dtos;
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

    public async Task<OtpRequest?> FindById(string id)
    {
        return await _otpRequests.FindAsync(id);
    }

    public async Task<GetOtpRequestForRegisterDto?> GetByIdForRegister(string id)
    {
        var a = await _otpRequests
            .Where(_ => _.Id == id && _.Purpose == OtpPurpose.Register && _.IsUsed == false)
            .Select(_ => new GetOtpRequestForRegisterDto()
            {
                CreatedAt = _.CreatedAt,
                ExpireAt = _.ExpireAt,
                Purpose = _.Purpose,
                Mobile = _.Mobile,
                OtpCode = _.OtpCode
            }).FirstOrDefaultAsync();
        return a;
    }

    public async Task<GetOtpRequestForRegisterDto?> GetByIdForResetPassword(string id)
    {
        return await _otpRequests
            .Where(_ => _.Id == id && _.Purpose == OtpPurpose.ResetPassword && _.IsUsed == false)
            .Select(_ => new GetOtpRequestForRegisterDto()
            {
                CreatedAt = _.CreatedAt,
                ExpireAt = _.ExpireAt,
                Purpose = _.Purpose,
                Mobile = _.Mobile,
                OtpCode = _.OtpCode
            }).FirstOrDefaultAsync();
    }
}
