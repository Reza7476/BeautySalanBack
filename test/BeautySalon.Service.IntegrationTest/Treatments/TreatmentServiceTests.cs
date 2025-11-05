using BeautySalon.Services.Treatments.Contracts;
using BeautySalon.Test.Tool.Entities.Treatments;
using BeautySalon.Test.Tool.Infrastructure.Integration;
using FluentAssertions;
using Xunit;

namespace BeautySalon.Service.IntegrationTest.Treatments;
public class TreatmentServiceTests:BusinessIntegrationTest
{
    private readonly ITreatmentService _sut;
    
    public TreatmentServiceTests()
    {
        _sut = TreatmentServiceFactory.Generate(DbContext);
    }

    [Fact]
    public async Task GetAll_should_return_all_treatment_properly()
    {
        var treat1 = new TreatmentBuilder()
            .WithTitle("title1")
            .WithDescription("description1")
            .WithImage()
            .Build();
        Save(treat1);
        var treat2 = new TreatmentBuilder()
            .WithTitle("title2")
            .WithDescription("description2")
            .WithImage()
            .Build();
        Save(treat2);

        var expected = await _sut.GetAll();

        expected.TotalElements.Should().Be(2);
        expected.Elements.First().Title.Should().Be(treat1.Title);
        expected.Elements.First().Description.Should().Be(treat1.Description);
        expected.Elements.First().Media.ImageName.Should().Be(treat1.Images.First().ImageName);
        expected.Elements.First().Media.UniqueName.Should().Be(treat1.Images.First().ImageUniqueName);
        expected.Elements.First().Media.URL.Should().Be(treat1.Images.First().URL);
        expected.Elements.First().Media.Extension.Should().Be(treat1.Images.First().Extension);
        expected.Elements.Last().Title.Should().Be(treat2.Title);
        expected.Elements.Last().Description.Should().Be(treat2.Description);
        expected.Elements.Last().Media.ImageName.Should().Be(treat2.Images.First().ImageName);
        expected.Elements.Last().Media.UniqueName.Should().Be(treat2.Images.First().ImageUniqueName);
        expected.Elements.Last().Media.URL.Should().Be(treat2.Images.First().URL);
        expected.Elements.Last().Media.Extension.Should().Be(treat2.Images.First().Extension);
    }

    [Fact]
    public async Task GetDetails_should_return_treatment_properly()
    {
        var treat1 = new TreatmentBuilder()
            .WithTitle("title1")
            .WithDescription("description1")
            .WithImage()
            .WithDuration(30)
            .WithPrice(1254.0m)
            .Build();
        Save(treat1);

        var expected = await _sut.GetDetails(treat1.Id);

        expected!.Title.Should().Be(treat1.Title);
        expected.Description.Should().Be(treat1.Description);
        expected.Duration.Should().Be(treat1.Duration);
        expected.Price.Should().Be(treat1.Price);
        expected.Media.First().UniqueName.Should().Be(treat1.Images.First().ImageUniqueName);
        expected.Media.First().ImageName.Should().Be(treat1.Images.First().ImageName);
        expected.Media.First().Extension.Should().Be(treat1.Images.First().Extension);
        expected.Media.First().URL.Should().Be(treat1.Images.First().URL);
    }

    [Fact]
    public async Task GetForLanding_should_return_treatments_for_landing_properly()
    {
        var treatment = new TreatmentBuilder()
            .WithTitle("title")
            .WithDescription("description")
            .WithImage()
            .Build();
        Save(treatment);

        var expected = await _sut.GetForLanding();

        expected.First().Title.Should().Be(treatment.Title);
        expected.First().Description.Should().Be(treatment.Description);
        expected.First().Id.Should().Be(treatment.Id);
        expected.First().Media!.UniqueName.Should().Be(treatment.Images.First().ImageUniqueName);
        expected.First().Media!.ImageName.Should().Be(treatment.Images.First().ImageName);
        expected.First().Media!.URL.Should().Be(treatment.Images.First().URL);
        expected.First().Media!.Extension.Should().Be(treatment.Images.First().Extension);
        expected.First().Media!.Id.Should().Be(treatment.Images.First().Id);
    }

    [Fact]
    public async Task GetAllForAppointment_should_return_all_treatment()
    {
        var treatment = new TreatmentBuilder()
            .WithTitle("abc")
            .Build();
        Save(treatment);

        var expected = await _sut.GetAllForAppointment();

        expected.First().Title.Should().Be(treatment.Title);
        expected.First().Id.Should().Be(treatment.Id);
    }

    [Fact]
    public async Task GetDetailsForAppointment_should_return_treatment_details_properly()
    {
        var treatment = new TreatmentBuilder()
            .WithTitle("abc")
            .WithDescription("abcD")
            .WithImage()
            .WithPrice(11223m)
            .WithDuration(180)
            .Build();
        Save(treatment);
        
        var expected = await _sut.GetDetailsForAppointment(treatment.Id);

        expected!.Description.Should().Be(treatment.Description);
        expected.Title.Should().Be(treatment.Title);
        expected.Duration.Should().Be(treatment.Duration);
        expected.Price.Should().Be(treatment.Price);
        expected.Image.ImageName.Should().Be(treatment.Images.First().ImageName);
        expected.Image.UniqueName.Should().Be(treatment.Images.First().ImageUniqueName);
        expected.Image.Extension.Should().Be(treatment.Images.First().Extension);
        expected.Image.URL.Should().Be(treatment.Images.First().URL);
    }
}
