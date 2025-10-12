using BeautySalon.Common.Dtos;

namespace BeautySalon.Services.WhyUsSections.Contracts.Dto;
public class GetWhyUsSectionDto
{
    public long Id { get; set; }
    public required string Title { get; set; }
    public string Description { get; set; } = default!;
    public ImageDetailsDto Image { get; set; } = default!;
    public List<GetWhyUsQuestionsDto> Questions { get; set; } = new();
}
