using BeautySalon.Entities.Commons;
using BeautySalon.Entities.ContactUs;
using System.Security.AccessControl;

namespace BeautySalon.Test.Tool.Entities.ContactUs;
public class AboutUsBuilder
{
    private readonly AboutUs _aboutUs;
    public AboutUsBuilder()
    {
        _aboutUs = new AboutUs()
        {
            Address = "address",
            CreateDate = DateTime.Now,
            Description = "description",
            Latitude = 0.12,
            Longitude = 0.13,
            MobileNumber = "mobileNumber",
            Telephone = "telephone",
            Email="email",
            Instagram="Instagram"
        };
    }


    public AboutUsBuilder WithEmail( string email)
    {
        _aboutUs.Email= email;
        return this;
    }

    public AboutUsBuilder WithInstagram(string instgram)
    {
        _aboutUs.Instagram = instgram;
        return this;
    }

    public AboutUsBuilder WithLogoDetils()
    {
        _aboutUs.LogoImage = new MediaDocument()
        {
            Extension="extension",
            ImageName="imageName",
            UniqueName="uniqueName",
            URL="url"
        };
        return this;
    }

    public AboutUsBuilder WithAddress(string address)
    {
        _aboutUs.Address = address;
        return this;
    }

    public AboutUsBuilder WithDescription(string description)
    {
        _aboutUs.Description = description;
        return this;
    }

    public AboutUsBuilder WithLatitude(double latitude)
    {
        _aboutUs.Latitude = latitude;
        return this;
    }

    public AboutUsBuilder WithLongitude(double longitude)
    {
        _aboutUs.Longitude = longitude;
        return this;
    }

    public AboutUsBuilder WithMobileNumber(string mobilenumber)
    {
        _aboutUs.MobileNumber = mobilenumber;
        return this;
    }

    public AboutUsBuilder WithTelephone(string telephone)
    {
        _aboutUs.Telephone = telephone;
        return this;
    }

    public AboutUs Build()
    {
        return _aboutUs;
    }

}
