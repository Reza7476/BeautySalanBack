namespace BeautySalon.Services.Banners.Contracts.Dto;
public class AddBannerDto
{
    public required string Title { get; set; }
    public required string UniqueName { get; set; }
    public required string ImageName { get; set; }
    public required string Extension { get; set; }
    public required string URL { get; set; }
}
