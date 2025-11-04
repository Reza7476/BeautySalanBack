namespace BeautySalon.Services.Users.Contracts.Dtos;
public class EditClientProfileDto
{
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public required string UserName { get; set; }
    public DateTime? BirthDateGregorian { get; set; }
}
