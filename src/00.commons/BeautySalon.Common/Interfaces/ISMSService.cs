
using BeautySalon.Common.Dtos;

namespace BeautySalon.Common.Interfaces;
public interface ISMSService : IScope
{
    Task<GetSMSumberCreditDto?> GetSMSCountCredit();
    Task<SendSMSResponseDto?> SendSMS(SendSMSDto dto);
    Task<VerifySMSDto?> VerifySMS(long recId);
}
