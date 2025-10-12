using BeautySalon.Common.Dtos;

namespace BeautySalon.Services.WhyUsSections.Contracts.Dto;
public class UpdateWhyUsSectionDto
{
    public required string Title { get; set; }
    public required MediaDto Media { get; set; }
}
