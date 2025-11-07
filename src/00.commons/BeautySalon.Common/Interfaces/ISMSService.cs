
using BeautySalon.Common.Dtos;

namespace BeautySalon.Common.Interfaces;
public interface ISMSService : IScope
{
    Task<SendSMSResponseDto?> SendSMS(SendSMSDto dto);
    Task<SendSMSResponseDto?> SendSMSSpecial(SendSMSSpecialDto dto);
    Task<VerifySMSDto?> VerifySMS(long recId);
}
