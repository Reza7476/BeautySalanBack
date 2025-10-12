using BeautySalon.Common.Dtos;
using BeautySalon.Services.WhyUsSections.Contracts.Dto;

namespace BeautySalon.Test.Tool.Entities.WhyUsSections;
public class AddWhyUsSectionDtoBuilder
{
    private readonly AddWhyUsSectionDto _dto;

    public AddWhyUsSectionDtoBuilder()
    {
        _dto = new AddWhyUsSectionDto()
        {
            Title = "Title",
            Description="description",
            Media = new MediaDto()
            {
                Extension = "extension",
                URL = "filePath",
                ImageName = "imageName",
                UniqueName = "uniqueName"
            }
        };
    }

    public AddWhyUsSectionDtoBuilder WithTitle(string title)
    {
        _dto.Title = title;
        return this;
    }

    public AddWhyUsSectionDtoBuilder WithDescription(string description)
    {
        _dto.Description = description;
        return this;
    }

    public AddWhyUsSectionDtoBuilder WithMedia()
    {
        _dto.Media = new MediaDto()
        {
            Extension = "extension",
            URL = "filePath",
            ImageName = "imageName",
            UniqueName = "uniqueName"
        };
        return this;
    }

    public AddWhyUsSectionDto Build()
    {
        return _dto;
    }
}
