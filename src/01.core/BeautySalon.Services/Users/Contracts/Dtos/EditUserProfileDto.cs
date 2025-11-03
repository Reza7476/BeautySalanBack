namespace BeautySalon.Services.Users.Contracts.Dtos;
public class EditUserProfileDto
{
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public DateTime? BirthDate { get; set; }

}
