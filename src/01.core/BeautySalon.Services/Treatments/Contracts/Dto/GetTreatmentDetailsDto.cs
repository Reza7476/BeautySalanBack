using BeautySalon.Common.Dtos;

namespace BeautySalon.Services.Treatments.Contracts.Dto;
public class GetTreatmentDetailsDto
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public List<MediaDto> Media { get; set; } = default!;
}
