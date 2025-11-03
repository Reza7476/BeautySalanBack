namespace BeautySalon.Services.Users.Contracts.Dtos;
public class EditAdminProfileDto
{
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public DateTime? BirthDate { get; set; }

}
