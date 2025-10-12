using BeautySalon.Services.WhyUsSections.Contracts.Dto;

namespace BeautySalon.Test.Tool.Entities.WhyUsSections;
public class AddWhyUsQuestionDtoBuilder
{
    private readonly AddWhyUsQuestionDto _dto;

    public AddWhyUsQuestionDtoBuilder()
    {
        _dto = new AddWhyUsQuestionDto()
        {
            Answer = "answer",
            Question = "question"
        };
    }

    public AddWhyUsQuestionDtoBuilder WithQuestion(string question)
    {
        _dto.Question = question;
        return this;
    }

    public AddWhyUsQuestionDtoBuilder WithAnswer(string answer)
    {
        _dto.Answer = answer;
        return this;
    }

    public AddWhyUsQuestionDto Build()
    {
        return _dto;
    }
}

