using System.Security.Principal;

namespace BeautySalon.Common.Dtos;
public class SendSMSResponseDto
{
    public long  RecId { get; set; }
    public string? Status { get; set; }
}
