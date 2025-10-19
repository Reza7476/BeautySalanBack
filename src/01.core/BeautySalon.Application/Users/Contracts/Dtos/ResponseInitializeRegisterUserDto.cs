namespace BeautySalon.Application.Users.Contracts.Dtos;
public class ResponseInitializeRegisterUserDto
{
    public string? OtpRequestId { get; set; }
    public int VerifyStatusCode { get; set; }
    public string? VerifyStatus { get; set; }
}
