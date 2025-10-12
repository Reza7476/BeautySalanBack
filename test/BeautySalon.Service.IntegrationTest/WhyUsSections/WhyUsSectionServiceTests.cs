using BeautySalon.Services.WhyUsSections.Contracts;
using BeautySalon.Test.Tool.Entities.WhyUsSections;
using BeautySalon.Test.Tool.Infrastructure.Integration;
using FluentAssertions;
using Xunit;

namespace BeautySalon.Service.IntegrationTest.WhyUsSections;
public class WhyUsSectionServiceTests : BusinessIntegrationTest
{
    private readonly IWhyUsSectionService _sut;

    public WhyUsSectionServiceTests()
    {
        _sut = WhyUsSectionServiceFactory.Generate(DbContext);
    }

    [Fact]
    public async Task GetQuestionsBySectionId_should_return_section_question_properly()
    {
        var section = new WhyUsSectionBuilder()
            .Build();
        Save(section);
        var question = new WhyUsQuestionBuilder()
            .WithSectionId(section.Id)
            .WithAuestion("question")
            .WithAnswer("answer")
            .Build();
        Save(question);

        var expected = await _sut.GetQuestionsBySectionId(section.Id);

        expected.First().Id.Should().Be(question.Id);
        expected.First().Answer.Should().Be(question.Answer);
    }

    [Fact]
    public async Task GetWhyUsSection_should_return_why_us_section_properly()
    {
        var section = new WhyUsSectionBuilder()
            .WithTitle("title")
            .WithDescription("description")
            .WithQuestion()
            .WithMedia()
            .Build();
        Save(section);

        var expected = await _sut.GetWhyUsSection();

        expected!.Title.Should().Be(section.Title);
        expected.Description.Should().Be(section.Description);
        expected.Image.Extension.Should().Be(section.Image.Extension);
        expected.Image.ImageName.Should().Be(section.Image.ImageName);
        expected.Image.UniqueName.Should().Be(section.Image.UniqueName);
        expected.Image.URL.Should().Be(section.Image.URL);
        expected.Questions.First().Question.Should().Be(section.Why_Us_Questions.First().Question);
        expected.Questions.First().Answer.Should().Be(section.Why_Us_Questions.First().Answer);
        expected.Questions.First().Id.Should().Be(section.Why_Us_Questions.First().Id);
    }

    [Fact]
    public async Task GetWhyUsSectionForEditById_should_return_why_us_section_properly()
    {
        var section = new WhyUsSectionBuilder()
            .WithTitle("title")
            .WithDescription("description")
            .WithMedia()
            .Build();
        Save(section);

        var expected = await _sut.GetWhyUsSectionByIdForEdit(section.Id);

        expected!.Description.Should().Be(section.Description);
        expected.Title.Should().Be(section.Title);
        expected.Image!.Extension.Should().Be(section.Image.Extension);
        expected.Image.UniqueName.Should().Be(section.Image.UniqueName);
        expected.Image.URL.Should().Be(section.Image.URL);
        expected.Image.ImageName.Should().Be(section.Image.ImageName);
    }


    [Fact]
    public async Task GetForLanding_should_return_why_us_properly()
    {
        var whyUs = new WhyUsSectionBuilder()
            .WithTitle("title")
            .WithDescription("description")
            .WithMedia()
            .WithQuestion()
            .Build();
        Save(whyUs);

        var expected = await _sut.GetForLanding();

        expected!.Description.Should().Be(whyUs.Description);
        expected.Title.Should().Be(whyUs.Title);
        expected.Image.Extension.Should().Be(whyUs.Image.Extension);
        expected.Image.UniqueName.Should().Be(whyUs.Image.UniqueName);
        expected.Image.ImageName.Should().Be(whyUs.Image.ImageName);
        expected.Image.URL.Should().Be(whyUs.Image.URL);
        expected.Questions.First().Question.Should().Be(whyUs.Why_Us_Questions.First().Question);
        expected.Questions.First().Answer.Should().Be(whyUs.Why_Us_Questions.First().Answer);
        expected.Questions.First().Id.Should().Be(whyUs.Why_Us_Questions.First().Id);
    }
}
