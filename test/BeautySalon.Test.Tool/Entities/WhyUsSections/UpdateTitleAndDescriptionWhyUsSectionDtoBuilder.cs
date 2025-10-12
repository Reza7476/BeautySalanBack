using BeautySalon.Services.WhyUsSections.Contracts.Dto;

namespace BeautySalon.Test.Tool.Entities.WhyUsSections;
public class UpdateTitleAndDescriptionWhyUsSectionDtoBuilder
{
    private readonly EditTitleAndDescriptionWhyUsSectionDto _dto;
    public UpdateTitleAndDescriptionWhyUsSectionDtoBuilder()
    {
        _dto = new EditTitleAndDescriptionWhyUsSectionDto()
        {
            Title = "title",
            Description = "description",
        };
    }

    public UpdateTitleAndDescriptionWhyUsSectionDtoBuilder WithTitle(string title)
    {
        _dto.Title = title;
        return this;
    }

    public UpdateTitleAndDescriptionWhyUsSectionDtoBuilder WithDescription(string description)
    {
        _dto.Description = description;
        return this;
    }

    public EditTitleAndDescriptionWhyUsSectionDto Build()
    {
        return _dto;
    }
}
