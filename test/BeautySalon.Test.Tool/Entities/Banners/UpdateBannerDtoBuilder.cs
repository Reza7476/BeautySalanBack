using BeautySalon.Services.Banners.Contracts.Dto;

namespace BeautySalon.Test.Tool.Entities.Banners;
public class UpdateBannerDtoBuilder
{
    private readonly UpdateBannerDto _dto;

    public UpdateBannerDtoBuilder()
    {
        _dto = new UpdateBannerDto()
        {
            Title = "update-title",
            Extension="update-extension",
            URL="update-pathFile",
            ImageName="update-imageName",
            UniqueName="update-uniqueName",
        };
    }

    public UpdateBannerDtoBuilder WithTitle(string title)
    {
        _dto.Title=title;
        return this;
    }

    public UpdateBannerDtoBuilder WithExtension( string extension)
    {
        _dto.Extension=extension;
        return this;
    }

    public UpdateBannerDtoBuilder WithFilePath(string filePath)
    {
        _dto.URL=filePath;
        return  this;
    }

    public UpdateBannerDtoBuilder WithImageName( string imageName)
    {
        _dto.ImageName=imageName;
        return this;
    }

    public UpdateBannerDtoBuilder WithImageUniqueName(string uniqueName)
    {
        _dto.UniqueName=uniqueName;
        return this;
    }

    public UpdateBannerDto Build()
    {
        return _dto;
    }

}
