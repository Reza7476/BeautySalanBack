using BeautySalon.Common.Dtos;

namespace BeautySalon.Test.Tool.Common;
public class ImageDetailsDtoBuilder
{
    private readonly ImageDetailsDto _builder;

    public ImageDetailsDtoBuilder()
    {
        _builder = new ImageDetailsDto()
        {
            Extension = "Extension",
            ImageName="ImageName",
            UniqueName="Name",
            URL="data-url",
        };
    }

    public ImageDetailsDtoBuilder WithExtension(string extension)
    {
        _builder.Extension = extension; 
        return this;
    }

    public ImageDetailsDtoBuilder WithImageName(string imageName)
    {
        _builder.ImageName=imageName;
        return this;
    }

    public ImageDetailsDtoBuilder WithUniqueName(string uniqueName)
    {
        _builder.UniqueName = uniqueName;
        return this;
    }

    public ImageDetailsDtoBuilder WithUrl(string url)
    {
        _builder.URL=url;
        return this;
    }

    public ImageDetailsDto Build()
    {
        return _builder;
    }
}
