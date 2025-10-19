namespace BeautySalon.Services.JWTTokenService.Contracts.Dtos;
public class AddGenerateTokenDto
{
    public string? UserName { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Id { get; set; }
    public string? Mobile { get; set; }
    public string? Email { get; set; }
    public List<string> UserRoles { get; set; } = new();
}
