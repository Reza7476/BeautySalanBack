namespace BeautySalon.Services.Treatments.Contracts.Dto;
public class AddTreatmentDto
{
    public required string  Title { get; set; }
    public required string Description { get; set; }
    public required string URL { get; set; }
    public required string ImageUniqueName { get; set; }
    public required string ImageName  { get; set; }
    public required string Extension { get; set; }
    public int Duration { get; set; }
}
