using System.ComponentModel.DataAnnotations;

namespace BeautySalon.Services.Users.Contracts.Dtos;
public class ForgetPassStepTwoDto
{
    [Required]
    public string OtpCode { get; set; } = default!;

    [Required]
    public string NewPassword { get; set; } = default!;

    [Required] 
    public string OtpRequestId { get; set; } = default!;
}
