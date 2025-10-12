using BeautySalon.Common.Dtos;

namespace BeautySalon.Test.Tool.Common;
public class AddImageDetailsDtoBuilder
{
    private readonly ImageDetailsDto _builder;

    public AddImageDetailsDtoBuilder()
    {
        _builder = new ImageDetailsDto()
        {
            Extension = "extension",
            ImageName="imageName",
            UniqueName="uniqueName",
            URL="url",
        };
    }


    public AddImageDetailsDtoBuilder WithExtension(string extension)
    {
        _builder.Extension = extension; 
        return this;
    }

    public AddImageDetailsDtoBuilder WithImageName(string imageName)
    {
        _builder.ImageName=imageName;
        return this;
    }

    public AddImageDetailsDtoBuilder WithUniqueName(string unirqueName)
    {
        _builder.UniqueName = unirqueName;
        return this;
    }

    public AddImageDetailsDtoBuilder WithUrl(string url)
    {
        _builder.URL=url;
        return this;
    }

    public ImageDetailsDto Build()
    {
        return _builder;
    }
}
