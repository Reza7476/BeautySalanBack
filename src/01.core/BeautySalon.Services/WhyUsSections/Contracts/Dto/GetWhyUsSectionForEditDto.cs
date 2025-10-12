using BeautySalon.Common.Dtos;

namespace BeautySalon.Services.WhyUsSections.Contracts.Dto;
public class GetWhyUsSectionForEditDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public ImageDetailsDto? Image { get; set; }
}
