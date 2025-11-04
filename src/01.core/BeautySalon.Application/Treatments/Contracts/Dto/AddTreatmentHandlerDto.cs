using Microsoft.AspNetCore.Http;

namespace BeautySalon.Application.Treatments.Contracts.Dto;
public class AddTreatmentHandlerDto
{
    public required string Title { get; set; }
    public required  string Description { get; set; }
    public required IFormFile Media { get; set; }
    public decimal Price { get; set; }
    public int Duration { get; set; } = 180;
}
