using BeautySalon.Entities.Banners;

namespace BeautySalon.Test.Tool.Entities.Banners;
public class BannerBuilder
{
    private readonly Banner _banner;

    public BannerBuilder()
    {
        _banner = new Banner
        {
            CreateDate = DateTime.Now,
            Extension = ".jpg",
            ImageName = "dummy-name",
            ImageUniqueName = "dummy-uniqueName",
            URL = "file path",
            Title = "title"
        };
    }

    public BannerBuilder WithCreateDate()
    {
        _banner.CreateDate = DateTime.Now;
        return this;
    }

    public BannerBuilder WithExtension(string extension)
    {
        _banner.Extension = extension;
        return this;
    }
    public BannerBuilder WithImageName(string imageName)
    {
        _banner.ImageName = imageName;
        return this;
    }
    public BannerBuilder WithImageUniqName(string uniqueImageName)
    {
        _banner.ImageUniqueName = uniqueImageName;
        return this;
    }
    public BannerBuilder WithFilePath(string filePath)
    {
        _banner.URL = filePath;
        return this;
    }
    public BannerBuilder WithTitle(string title)
    {
        _banner.Title = title;
        return this;
    }

    public Banner Build()
    {
        return _banner;
    }


}
