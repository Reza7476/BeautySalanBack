using BeautySalon.Services.Treatments.Contracts.Dto;

namespace BeautySalon.Test.Tool.Entities.Treatments;
public class AddTreatmentDtoBuilder
{
    private readonly AddTreatmentDto _dto;

    public AddTreatmentDtoBuilder()
    {
        _dto = new AddTreatmentDto()
        {
            Description = "description",
            ImageName = "image",
            ImageUniqueName = "imageUnique",
            Title = "title",
            URL = "url",
            Extension = "extension",
            Duration=30,
            Price=1255.11m
        };
    }


    public AddTreatmentDtoBuilder withPrice(decimal price)
    {
        _dto.Price = price;
        return this;
    }

    public AddTreatmentDtoBuilder WithDuration(int duration)
    {
        _dto.Duration=duration;
        return this;
    }

    public AddTreatmentDtoBuilder WithExtension(string extension)
    {
        _dto.Extension = extension;
        return this;
    }

    public AddTreatmentDtoBuilder WithDescription(string description)
    {
        _dto.Description = description;
        return this;
    }

    public AddTreatmentDtoBuilder WithImageName(string imageName)
    {
        _dto.ImageName = imageName;
        return this;
    }

    public AddTreatmentDtoBuilder WithImageUniqueName(string imageUniqueName)
    {
        _dto.ImageUniqueName = imageUniqueName;
        return this;
    }

    public AddTreatmentDtoBuilder WithTitle(string title)
    {
        _dto.Title = title;
        return this;
    }

    public AddTreatmentDtoBuilder WithURL(string url)
    {
        _dto.URL = url;
        return this;
    }


    public AddTreatmentDto Build()
    {
        return _dto;
    }


}
