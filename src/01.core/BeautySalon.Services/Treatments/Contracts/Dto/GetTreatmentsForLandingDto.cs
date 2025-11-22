using BeautySalon.Common.Dtos;

namespace BeautySalon.Services.Treatments.Contracts.Dto;
public class GetTreatmentForLandingDto
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public long Id { get; set; }
    public double? Rate { get; set; }
    public MediaDto? Media { get; set; }
}
