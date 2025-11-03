using BeautySalon.Entities.WhyUsSections;
using BeautySalon.Services.WhyUsSections.Contracts;
using BeautySalon.Services.WhyUsSections.Exceptions;
using BeautySalon.Test.Tool.Common;
using BeautySalon.Test.Tool.Entities.WhyUsSections;
using BeautySalon.Test.Tool.Infrastructure.UnitTests;
using FluentAssertions;
using Xunit;

namespace BeautySalon.Service.UnitTest.WhyUsSections;
public class WhyUsSectionTests : BusinessUnitTest
{
    private readonly IWhyUsSectionService _sut;

    public WhyUsSectionTests()
    {
        _sut = WhyUsSectionServiceFactory.Generate(DbContext);
    }

    [Fact]
    public async Task Add_should_add_why_us_section_properly()
    {
        var dto = new AddWhyUsSectionDtoBuilder()
            .WithTitle("title")
            .WithMedia()
            .WithDescription("description")
            .Build();

        await _sut.Add(dto);

        var expected = ReadContext.Set<Why_Us_Section>().First();
        expected.Title.Should().Be(dto.Title);
        expected.Description.Should().Be(dto.Description);
        expected.Image.ImageName.Should().Be(dto.Media.ImageName);
        expected.Image.UniqueName.Should().Be(dto.Media.UniqueName);
        expected.Image.Extension.Should().Be(dto.Media.Extension);
        expected.Image.URL.Should().Be(dto.Media.URL);
    }

    [Fact]
    public async Task AddQuestions_should_add_why_us_question_properly()
    {
        var section = new WhyUsSectionBuilder()
            .Build();
        Save(section);
        var dto = new AddWhyUsQuestionDtoBuilder()
            .WithQuestion("question")
            .WithAnswer("answer")
            .Build();

        await _sut.AddQuestion(dto, section.Id);

        var expected = ReadContext.Set<Why_Us_Question>().First();
        expected.Question.Should().Be(dto.Question);
        expected.Answer.Should().Be(dto.Answer);
    }

    [Theory]
    [InlineData(-1)]
    public async Task AddQuestions_should_throw_exception_when_why_us_section_not_found(long sectionId)
    {
        var dto = new AddWhyUsQuestionDtoBuilder()
            .Build();

        Func<Task> expected = async () => await _sut.AddQuestion(dto, sectionId);

        await expected.Should().ThrowExactlyAsync<WhyUsSectionNotFoundException>();
    }

    [Fact]
    public async Task UpdateQuestion_should_update_question_properly()
    {
        var section = new WhyUsSectionBuilder()
            .Build();
        Save(section);
        var question = new WhyUsQuestionBuilder()
            .WithAuestion("question")
            .WithAnswer("answer")
            .WithSectionId(section.Id)
            .Build();
        Save(question);
        var dto = new UpdateWhyUsQuestionDtoBuilder()
            .WithQuestion("dummy")
            .WithAnswer("dummy")
            .Build();

        await _sut.UpdateQuestion(question.Id, dto);

        var expected = ReadContext.Set<Why_Us_Question>().First();
        expected.Question.Should().Be(dto.Question);
        expected.Answer.Should().Be(dto.Answer);
    }


    [Theory]
    [InlineData(-1)]
    public async Task UpdateQuestion_should_throw_exception_when_why_us_question_not_found(long questionId)
    {
        var dto = new UpdateWhyUsQuestionDtoBuilder()
            .Build();

        Func<Task> expected = async () => await _sut.UpdateQuestion(questionId, dto);

        await expected.Should().ThrowExactlyAsync<WhyUsQuestionNotFoundException>();
    }

    [Fact]
    public async Task UpdateWhyUsSection_should_update_title_description_section_properly()
    {
        var section = new WhyUsSectionBuilder()
            .WithTitle("title")
            .WithMedia()
            .Build();
        Save(section);
        var dto = new UpdateTitleAndDescriptionWhyUsSectionDtoBuilder()
            .WithTitle("title")
            .WithDescription("description")
            .Build();

        await _sut.UpdateWhyUsSection(section.Id, dto);

        var expected = ReadContext.Set<Why_Us_Section>().First();
        expected.Title.Should().Be(dto.Title);
        expected.Description.Should().Be(dto.Description);
    }

    [Theory]
    [InlineData(-1)]
    public async Task UpdateWhyUsSection_should_throw_exception_when_section_not_found(long id)
    {
        var dto = new UpdateTitleAndDescriptionWhyUsSectionDtoBuilder()
            .Build();
        Func<Task> expected = async () => await _sut.UpdateWhyUsSection(id, dto);
        
        await expected.Should().ThrowAsync<WhyUsSectionNotFoundException>();
    }




    [Fact]
    public async Task DeleteQuestion_should_remove_question_properly()
    {
        var section = new WhyUsSectionBuilder()
            .Build();
        Save(section);
        var question = new WhyUsQuestionBuilder()
            .WithSectionId(section.Id)
            .Build();
        Save(question);

        await _sut.DeleteQuestion(question.Id);

        var expected = ReadContext.Set<Why_Us_Question>().FirstOrDefault();
        expected.Should().BeNull();
    }

    [Theory]
    [InlineData(-1)]
    public async Task DeleteQuestion_should_throw_exception_when_question_not_found(long questionId)
    {
        Func<Task> expected = async () => await _sut.DeleteQuestion(questionId);
        await expected.Should().ThrowExactlyAsync<WhyUsQuestionNotFoundException>();
    }

    [Fact]
    public async Task UpdateImage_should_update_image_properly()
    {
        var whyUs = new WhyUsSectionBuilder()
            .WithMedia()
            .Build();
        Save(whyUs);
        var dto = new ImageDetailsDtoBuilder()
            .WithUniqueName("unique")
            .WithExtension(".jpeg")
            .WithImageName("imageName")
            .WithUrl("url")
            .Build();

        await _sut.UpdateImage(whyUs.Id, dto);

        var expected = ReadContext.Set<Why_Us_Section>().First();
        expected.Image.URL.Should().Be(dto.URL);
        expected.Image.ImageName.Should().Be(dto.ImageName);
        expected.Image.Extension.Should().Be(dto.Extension);
        expected.Image.UniqueName.Should().Be(dto.UniqueName);
    }
}
