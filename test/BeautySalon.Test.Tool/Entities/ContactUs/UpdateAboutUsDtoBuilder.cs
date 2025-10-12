using BeautySalon.Services.ContactUs.Contracts.Dto;

namespace BeautySalon.Test.Tool.Entities.ContactUs;
public class UpdateAboutUsDtoBuilder
{
    private readonly UpdateAboutUsDto _dto;

    public UpdateAboutUsDtoBuilder()
    {
        _dto = new UpdateAboutUsDto()
        {
            Address="address",
            Description="Description",
            Latitude=0.23,
            Longitude=0.356,
            MobileNumber="mobileNumber",
            Telephone="telephone",
            Email="email.com",
            Instagram="instagram"
        };
    }

    public UpdateAboutUsDtoBuilder WithEmail(string email)
    {
        _dto.Email = email;
        return this;
    }

    public UpdateAboutUsDtoBuilder WithInstagram(string instagram)
    {
        _dto.Instagram=instagram;
        return this;
    }


    public UpdateAboutUsDtoBuilder WithAddress(string address)
    {
        _dto.Address = address;
        return this;
    }

    public UpdateAboutUsDtoBuilder WithTelephone(string telephone)
    {
        _dto.Telephone = telephone;
        return this;
    }

    public UpdateAboutUsDtoBuilder WithDescription(string description)
    {
        _dto.Description = description;
        return this;
    }

    public UpdateAboutUsDtoBuilder WithMobileNumber(string mobileNumber)
    {
        _dto.MobileNumber = mobileNumber;
        return this;
    }

    public UpdateAboutUsDtoBuilder WithLongitude(double longitude)
    {
        _dto.Longitude = longitude;
        return this;
    }

    public UpdateAboutUsDtoBuilder WithLatitude(double latitude)
    {
        _dto.Latitude=latitude;
        return this;
    }

    public UpdateAboutUsDto Build()
    {
        return _dto;
    }
}
