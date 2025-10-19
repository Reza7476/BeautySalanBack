namespace BeautySalon.Common.Dtos;
public class SendSMSDto
{
    public string? Message { get; set; }
    public string Number { get; set; } = default!;
}
