using BeautySalon.Common.Dtos;
using BeautySalon.Services.ContactUs.Contracts.Dto;

namespace BeautySalon.Test.Tool.Entities.ContactUs;
public class AddAboutUsDtoBuilder
{
    private readonly AddAboutUsDto _dto;

    public AddAboutUsDtoBuilder()
    {
        _dto = new AddAboutUsDto()
        {
            Address = "address",
            Description = "description",
            Latitude = 0.001,
            Longitude = 00.12,
            MobileNumber = "mobile",
            Telephone = "telephone",
            Email="email",
            Instagram="Instagram",
        };
    }


    public AddAboutUsDtoBuilder WithEmail(string email)
    {
        _dto.Email = email;
        return this;    
    }

    public AddAboutUsDtoBuilder WithInstagram(string instagram)
    {
        _dto.Instagram = instagram;
        return this;    
    }


    public AddAboutUsDtoBuilder WithLogoDetails()
    {
        _dto.LogoDetails = new ImageDetailsDto()
        {
            Extension="extension",
            ImageName="imageName",
            UniqueName="uniqueName",
            URL ="url"
        };
        return this;    
    }

    public AddAboutUsDtoBuilder WithAddress(string address)
    {
        _dto.Address = address;
        return this;
    }

    public AddAboutUsDtoBuilder WithLongitude(double longitude)
    {
        _dto.Longitude = longitude;
        return this;
    }

    public AddAboutUsDtoBuilder WithLatitude(double latitude)
    {
        _dto.Latitude = latitude;
        return this;
    }


    public AddAboutUsDtoBuilder WithMobileNumber(string mobileNumber)
    {
        _dto.MobileNumber = mobileNumber;
        return this;
    }


    public AddAboutUsDtoBuilder WithTelephone(string telephone)
    {
        _dto.Telephone = telephone;
        return this;
    }

    public AddAboutUsDtoBuilder WithDescription(string description)
    {
        _dto.Description = description;
        return this;
    }

    public AddAboutUsDto Build()
    {
        return _dto;
    }
}
