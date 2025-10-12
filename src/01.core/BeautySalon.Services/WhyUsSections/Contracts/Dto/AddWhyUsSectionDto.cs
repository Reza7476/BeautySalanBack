using BeautySalon.Common.Dtos;

namespace BeautySalon.Services.WhyUsSections.Contracts.Dto;
public class AddWhyUsSectionDto
{
    public  required string  Title { get; set; }
    public  required string  Description { get; set; }
    public  required MediaDto Media { get; set; }
}
