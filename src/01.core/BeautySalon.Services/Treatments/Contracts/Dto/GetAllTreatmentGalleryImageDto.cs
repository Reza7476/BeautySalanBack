using BeautySalon.Common.Dtos;

namespace BeautySalon.Services.Treatments.Contracts.Dto;
public class GetAllTreatmentGalleryImageDto
{
    public string TreatmentTitle { get; set; } = default!;
    public List<ImageDetailsDto> Images { get; set; } = new();
}
