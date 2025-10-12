using BeautySalon.Entities.WhyUsSections;

namespace BeautySalon.Test.Tool.Entities.WhyUsSections;
public class WhyUsQuestionBuilder
{
    private readonly Why_Us_Question _builder;

    public WhyUsQuestionBuilder()
    {
        _builder = new Why_Us_Question()
        {
            Question = "question",
            Answer = "answer",
            CreateDate = DateTime.Now,
        };
    }

    public WhyUsQuestionBuilder WithAuestion(string question)
    {
        _builder.Question = question;
        return this;
    }

    public WhyUsQuestionBuilder WithAnswer(string answer)
    {
        _builder.Answer = answer;
        return this;
    }

    public WhyUsQuestionBuilder WithSectionId(long id)
    {
        _builder.SectionId = id;
        return this;
    }

    public Why_Us_Question Build()
    {
        return _builder;
    }
}
