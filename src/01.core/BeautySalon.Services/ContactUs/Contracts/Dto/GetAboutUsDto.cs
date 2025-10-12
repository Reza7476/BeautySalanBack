using BeautySalon.Entities.Commons;

namespace BeautySalon.Services.ContactUs.Contracts.Dto;
public class GetAboutUsDto
{
    public long Id { get; set; }
    public required string MobileNumber { get; set; }
    public string? Telephone { get; set; }
    public string? Address { get; set; }
    public string? Description { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public string? Email { get; set; }
    public string? Instagram { get; set; }
    public MediaDocument? LogoImage { get; set; }
}
