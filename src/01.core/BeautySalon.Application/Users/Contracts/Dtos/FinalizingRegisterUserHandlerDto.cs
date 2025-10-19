using System.ComponentModel.DataAnnotations;

namespace BeautySalon.Application.Users.Contracts.Dtos;
public class FinalizingRegisterUserHandlerDto
{
    [Required]
    public string Name { get; set; } = default!;

    [Required]
    public string LastName { get; set; } = default!;

    [Required]
    public string UserName { get; set; } = default!;

    [Required]
    public string Password { get; set; } = default!;

    [Required]
    public string OtpRequestId { get; set; } = default!;

    [Required]
    public string OtpCode { get; set; } = default!;

    public string? Email { get; set; }

}
