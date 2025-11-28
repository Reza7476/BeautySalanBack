using BeautySalon.Services.ContactUs.Contracts;
using BeautySalon.Test.Tool.Entities.ContactUs;
using BeautySalon.Test.Tool.Infrastructure.Integration;
using FluentAssertions;
using Xunit;

namespace BeautySalon.Service.IntegrationTest.ContactUs;
public class AboutUsServiceTest : BusinessIntegrationTest
{
    private readonly IAboutUsService _sut;
    public AboutUsServiceTest()
    {
        _sut = AboutUsServiceFactory.Generate(DbContext);
    }

    [Fact]
    public async Task Get_should_return_about_us_properly()
    {
        var aboutUs = new AboutUsBuilder()
            .WithTelephone("telephone")
            .WithLatitude(0.12)
            .WithLongitude(.012)
            .WithDescription("description")
            .WithAddress("address")
            .WithMobileNumber("mobile")
            .WithEmail("email")
            .WithInstagram("Instagram")
            .WithLogoDetils()
            .Build();
        Save(aboutUs);

        var expected = await _sut.Get();

        expected!.Id.Should().Be(aboutUs.Id);
        expected.Address.Should().Be(aboutUs.Address);
        expected.Latitude.Should().Be(aboutUs.Latitude);
        expected.Telephone.Should().Be(aboutUs.Telephone);
        expected.Longitude.Should().Be(aboutUs.Longitude);
        expected.Description.Should().Be(aboutUs.Description);
        expected.MobileNumber.Should().Be(aboutUs.MobileNumber);
        expected.Instagram.Should().Be(aboutUs.Instagram);
        expected.Email.Should().Be(aboutUs.Email);
        expected.LogoImage!.URL.Should().Be(aboutUs.LogoImage!.URL);
        expected.LogoImage.UniqueName.Should().Be(aboutUs.LogoImage.UniqueName);
        expected.LogoImage.ImageName.Should().Be(aboutUs.LogoImage.ImageName);
        expected.LogoImage.Extension.Should().Be(aboutUs.LogoImage.Extension);
    }

    [Fact]
    public async Task GetById_should_return_about_us_properly()
    {
        var aboutUs = new AboutUsBuilder()
            .WithAddress("address")
            .WithLatitude(29.12122)
            .WithLongitude(52.3698)
            .WithEmail("email")
            .WithLogoDetils()
            .WithDescription("description")
            .WithInstagram("instagram")
            .WithMobileNumber("9174367476")
            .WithTelephone("7132273599")
            .Build();
        Save(aboutUs);

        var expected = await _sut.GetById(aboutUs.Id);

        expected!.MobileNumber.Should().Be(aboutUs.MobileNumber);
        expected.Telephone.Should().Be(aboutUs.Telephone);
        expected.Email.Should().Be(aboutUs.Email);
        expected.Instagram.Should().Be(aboutUs.Instagram);
        expected.Latitude.Should().Be(aboutUs.Latitude);
        expected.Longitude.Should().Be(aboutUs.Longitude);
        expected.Address.Should().Be(aboutUs.Address);
        expected.Description.Should().Be(aboutUs.Description);
        expected.LogoImage!.URL.Should().Be(aboutUs.LogoImage!.URL);
        expected.LogoImage.Extension.Should().Be(aboutUs.LogoImage.Extension);
        expected.LogoImage.ImageName.Should().Be(aboutUs.LogoImage.ImageName);
        expected.LogoImage.UniqueName.Should().Be(aboutUs.LogoImage.UniqueName);
    }
}
