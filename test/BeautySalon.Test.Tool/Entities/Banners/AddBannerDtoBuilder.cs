using BeautySalon.Services.Banners.Contracts.Dto;

namespace BeautySalon.Test.Tool.Entities.Banners;
public class AddBannerDtoBuilder
{
    private readonly AddBannerDto _dto;

    public AddBannerDtoBuilder()
    {
        _dto = new AddBannerDto()
        {
            Extension = ".jpg",
            URL = "dummy-file-path",
            ImageName = "dummy-image-name",
            Title = "title",
            UniqueName = "dummy-uniqueName"
        };
    }

    public AddBannerDtoBuilder WithExtension(string extension)
    {
        _dto.Extension = extension;
        return this;
    }

    public AddBannerDtoBuilder WithFilePath(string filePath)
    {
        _dto.URL = filePath;
        return this;

    }

    public AddBannerDtoBuilder WithImageName(string name)
    {

        _dto.ImageName = name;
        return this;

    }

    public AddBannerDtoBuilder WithTitle(string title)
    {
        _dto.Title = title;
        return this;
    }

    public AddBannerDtoBuilder WithUniiqueName(string uniqueName)
    {
        _dto.UniqueName = uniqueName;
        return this;
    }

    public AddBannerDto Build()
    {
        return _dto;
    }
}
