namespace BeautySalon.Services.Treatments.Contracts.Dto;
public class UpdateTreatmentDto
{
    public  required string Title { get; set; }
    public  required string Description { get; set; }
    public int Duration { get; set; }
    public decimal Price { get; set; }
}
