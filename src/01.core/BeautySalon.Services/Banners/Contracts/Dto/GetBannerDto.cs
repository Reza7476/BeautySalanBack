namespace BeautySalon.Services.Banners.Contracts.Dto;
public class GetBannerDto
{
    public long Id { get; set; }
    public required string Title { get; set; }
    public required string ImageName { get; set; }
    public required string ImageUniqueName { get; set; }
    public required string Extension { get; set; }
    public required string URL { get; set; }
    public DateTime CreateDate { get; set; }


}
