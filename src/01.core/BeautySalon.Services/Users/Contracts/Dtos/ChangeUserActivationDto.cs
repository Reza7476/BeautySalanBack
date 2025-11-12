namespace BeautySalon.Services.Users.Contracts.Dtos;
public class ChangeUserActivationDto
{
    public string UserId { get; set; } = default!;
    public bool IsActive { get; set; }
}
