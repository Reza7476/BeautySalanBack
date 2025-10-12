using BeautySalon.Services.WhyUsSections.Contracts.Dto;

namespace BeautySalon.Test.Tool.Entities.WhyUsSections;
public  class UpdateWhyUsQuestionDtoBuilder
{
    private readonly UpdateWhyUsQuestionDto _dto;
    public UpdateWhyUsQuestionDtoBuilder()
    {
        _dto = new UpdateWhyUsQuestionDto()
        {
            Answer="answer",
            Question="question"
        };
    }

    public UpdateWhyUsQuestionDtoBuilder WithQuestion(string question)
    {
        _dto.Question=question;
        return this;
    }

    public UpdateWhyUsQuestionDtoBuilder WithAnswer(string answer)
    {
        _dto.Answer=answer; 
        return this;
    }

    public UpdateWhyUsQuestionDto Build()
    {
        return _dto;
    }
}
