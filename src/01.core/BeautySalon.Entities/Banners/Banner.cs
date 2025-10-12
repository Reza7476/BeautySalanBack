namespace BeautySalon.Entities.Banners;
public class Banner
{
    public long Id { get; set; }
    public required string Title { get; set; }
    public required string ImageName { get; set; }
    public required string ImageUniqueName { get; set; }
    public required string Extension { get; set; }
    public required string URL { get; set; }
    public required DateTime CreateDate { get; set; }
}
