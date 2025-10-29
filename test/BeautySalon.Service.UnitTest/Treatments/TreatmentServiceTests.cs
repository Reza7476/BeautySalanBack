using BeautySalon.Entities.Treatments;
using BeautySalon.Services.Treatments.Contracts;
using BeautySalon.Services.Treatments.Exceptions;
using BeautySalon.Test.Tool.Common;
using BeautySalon.Test.Tool.Entities.Treatments;
using BeautySalon.Test.Tool.Infrastructure.UnitTests;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BeautySalon.Service.UnitTest.Treatments;
public class TreatmentServiceTests : BusinessUnitTest
{

    private readonly ITreatmentService _sut;
    public TreatmentServiceTests()
    {
        _sut = TreatmentServiceFactory.Generate(DbContext);
    }

    [Fact]
    public async Task Add_should_add_treatment_properly()
    {
        var dto = new AddTreatmentDtoBuilder()
            .WithURL("url")
            .WithTitle("title")
            .WithDescription("description")
            .WithImageName("imageName")
            .WithDuration(45)
            .WithImageUniqueName("unique")
            .Build();

        await _sut.Add(dto);

        var expected = ReadContext.Set<Treatment>().Include(_ => _.Images).FirstOrDefault();
        var expectedImage = expected!.Images.FirstOrDefault();
        expected!.Title.Should().Be(dto.Title);
        expected.Duration.Should().Be(dto.Duration);
        expected.Description.Should().Be(dto.Description);
        expectedImage!.ImageName.Should().Be(dto.ImageName);
        expectedImage.ImageUniqueName.Should().Be(dto.ImageUniqueName);
        expectedImage.URL.Should().Be(dto.URL);
    }

    [Fact]
    public async Task AddImage_should_add_image_properly()
    {
        var treatment = new TreatmentBuilder()
            .Build();
        Save(treatment);
        var dto = new AddImageDetailsDtoBuilder()
            .WithUniqueName("name")
            .WithExtension("extension")
            .WithImageName("name")
            .WithUrl("url")
            .Build();
        await _sut.AddImageReturnImageId(treatment.Id, dto);

        var expected = ReadContext.Set<TreatmentImage>().First();

        expected.URL.Should().Be(dto.URL);
        expected.ImageName.Should().Be(dto.ImageName);
        expected.ImageUniqueName.Should().Be(dto.UniqueName);
        expected.Extension.Should().Be(dto.Extension);
    }

    [Theory]
    [InlineData(-1)]
    public async Task AddImageReturnImageId_should_throw_exception_when_treatment_not_found(long id)
    {
        var dto = new AddImageDetailsDtoBuilder()
            .Build();

        Func<Task> expected = async () => await _sut.AddImageReturnImageId(id, dto);

        await expected.Should().ThrowExactlyAsync<TreatmentNotFoundException>();
    }

    [Fact]
    public async Task GetUrl_Remove_Image_should_remove_image()
    {
        var treatment = new TreatmentBuilder()
            .WithImage()
            .WithImage()
            .Build();
        Save(treatment);

        await _sut.GetUrl_Remove_Image(treatment.Images.First().Id, treatment.Id);

        var expect = ReadContext.Set<TreatmentImage>().ToList();
        expect!.Count.Should().Be(1);
    }

    [Theory]
    [InlineData(-1, -1)]
    public async Task GetUrl_Remove_Image_should_throw_exception_when_treatment_not_found(long id, long imageId)
    {

        Func<Task> expected = async () => await _sut.GetUrl_Remove_Image(imageId, id);

        await expected.Should().ThrowExactlyAsync<TreatmentNotFoundException>();
    }

    [Fact]
    public async Task GetUrl_Remove_Image_should_expected_when_it_has_only_one_image()
    {
        var treatment = new TreatmentBuilder()
            .WithImage()
            .Build();
        Save(treatment);

        Func<Task> expected = async () => await _sut.GetUrl_Remove_Image(treatment.Images.First().Id, treatment.Id);

        await expected.Should().ThrowAsync<NotAllowedDeleteImageException>();
    }


    [Fact]
    public async Task Update_should_update_treatment_properly()
    {
        var treatment = new TreatmentBuilder()
            .WithTitle("title")
            .WithDescription("description")
            .WithDuration(30)
            .Build();
        Save(treatment);
        var dto = new UpdateTreatmentDtoBuilder()
            .WithDescription("description")
            .WithTitle("title")
            .WithDuration(45)
            .Build();

        await _sut.Update(dto, treatment.Id);

        var expected = ReadContext.Set<Treatment>().FirstOrDefault();
        expected!.Title.Should().Be(dto.Title);
        expected.Description.Should().Be(dto.Description);
        expected.Duration.Should().Be(dto.Duration);
    }


    [Theory]
    [InlineData(-1)]
    public async Task Update_should_throw_exception_when_treatment_not_found(long id)
    {
        var dto = new UpdateTreatmentDtoBuilder()
            .Build();
        Func<Task> expected = async () => await _sut.Update(dto, id);
        await expected.Should().ThrowExactlyAsync<TreatmentNotFoundException>();
    }

}
