namespace BeautySalon.Common.Dtos;

public class MediaDto
{
    public required string UniqueName{ get; set; }
    public required string ImageName { get; set; }
    public required string Extension { get; set; }
    public required string URL { get; set; }
    public long  Id { get; set; }   
}
