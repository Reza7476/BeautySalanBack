using BeautySalon.Common.Dtos;

namespace BeautySalon.Services.Treatments.Contracts.Dto;
public class GetAllTreatmentsDto
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public long Id { get; set; }
    public MediaDto Media { get; set; } = default!;
    public decimal Price { get; set; }
}
