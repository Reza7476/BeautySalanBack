using BeautySalon.Services.Banners.Contracts;
using BeautySalon.Test.Tool.Entities.Banners;
using BeautySalon.Test.Tool.Infrastructure.Integration;
using FluentAssertions;
using Xunit;

namespace BeautySalon.Service.IntegrationTest.Banners;
public class BannerServiceTests : BusinessIntegrationTest
{
    private readonly IBannerService _sut;

    public BannerServiceTests()
    {
        _sut = BannerServiceFactory.Generate(SetupContext);
    }

    [Fact]
    public async Task Get_should_get_banner_properly()
    {
        var banner = new BannerBuilder()
            .WithExtension("extension")
            .WithTitle("title")
            .WithCreateDate()
            .WithFilePath("filePath")
            .WithImageUniqName("uniqueName")
            .WithImageName("imageName")
            .Build();
        Save(banner);

        var expected = await _sut.Get();

        expected!.URL.Should().Be(banner.URL);
        expected.Extension.Should().Be(banner.Extension);
        expected.Title.Should().Be(banner.Title);
        expected.CreateDate.Should().Be(banner.CreateDate);
        expected.ImageUniqueName.Should().Be(banner.ImageUniqueName);
        expected.ImageName.Should().Be(banner.ImageName);   
    }

    [Fact]
    public async Task GetById_should_return_Banner()
    {
        var banner = new BannerBuilder()
            .WithExtension("extension")
            .WithTitle("title")
            .WithCreateDate()
            .WithFilePath("filePath")
            .WithImageUniqName("uniqueName")
            .WithImageName("imageName")
            .Build();
        Save(banner);

        var expected = await _sut.GetById(banner.Id);

        expected!.URL.Should().Be(banner.URL);
        expected.Extension.Should().Be(banner.Extension);
        expected.Title.Should().Be(banner.Title);
        expected.CreateDate.Should().Be(banner.CreateDate);
        expected.ImageUniqueName.Should().Be(banner.ImageUniqueName);
        expected.ImageName.Should().Be(banner.ImageName);

    }
}
