using BeautySalon.Entities.Banners;
using BeautySalon.Services.Banners.Contracts;
using BeautySalon.Services.Banners.Exceptions;
using BeautySalon.Test.Tool.Entities.Banners;
using BeautySalon.Test.Tool.Infrastructure.UnitTests;
using FluentAssertions;
using Xunit;

namespace BeautySalon.Service.UnitTest.Banners;
public class BannerServiceTests : BusinessUnitTest
{
    private readonly IBannerService _sut;

    public BannerServiceTests()
    {
        _sut = BannerServiceFactory.Generate(SetupContext);
    }

    [Fact]
    public async Task AddAdd_should_add_banner_properly()
    {
        var dto = new AddBannerDtoBuilder()
            .WithFilePath("pathName")
            .WithExtension(".jpeg")
            .WithTitle("title")
            .WithUniiqueName("uniqueName")
            .Build();

        await _sut.Add(dto);

        var expected = ReadContext.Set<Banner>().First();
        expected.URL.Should().Be(dto.URL);
        expected.Extension.Should().Be(dto.Extension);
        expected.ImageName.Should().Be(dto.ImageName);
        expected.ImageUniqueName.Should().Be(dto.UniqueName);
        expected.Title.Should().Be(dto.Title);
    }


    [Fact]
    public async Task UpdateBanner_should_update_banner_title_properly()
    {
        var banner = new BannerBuilder()
            .WithTitle("title")
            .WithExtension("extension")
            .WithFilePath("filePath")
            .WithImageName("imageName")
            .WithImageUniqName("imageUniqName")
            .Build();
        Save(banner);
        var dto = new UpdateBannerDtoBuilder()
            .WithTitle("newTitle")
            .WithFilePath("newFilePath")
            .WithExtension("newExtension")
            .WithImageName("newImageName")
            .WithImageUniqueName("imageUniqueName")
            .Build();

        await _sut.Update(banner.Id, dto);

        var expected = ReadContext.Set<Banner>().First();
        expected.Title.Should().Be(dto.Title);
        expected.Extension.Should().Be(dto.Extension);
        expected.ImageName.Should().Be(dto.ImageName);
        expected.ImageUniqueName.Should().Be(dto.UniqueName);
        expected.URL.Should().Be(dto.URL);
    }

    [Theory]
    [InlineData(-1)]
    public async Task UpdateBanner_should_throw_exception_when_banner_not_found(long id)
    {
        var dto = new UpdateBannerDtoBuilder()
            .Build();

        Func<Task> expected = async () => await _sut.Update(id, dto);
        
        await expected.Should().ThrowExactlyAsync<BannerNotFoundException>();
    }
}
