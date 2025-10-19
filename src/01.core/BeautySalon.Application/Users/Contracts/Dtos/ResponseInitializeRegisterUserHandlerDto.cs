namespace BeautySalon.Application.Users.Contracts.Dtos;
public class ResponseInitializeRegisterUserHandlerDto
{
    public string? OtpRequestId { get; set; }
    public int VerifyStatusCode { get; set; }
    public string? VerifyStatus { get; set; }
}
