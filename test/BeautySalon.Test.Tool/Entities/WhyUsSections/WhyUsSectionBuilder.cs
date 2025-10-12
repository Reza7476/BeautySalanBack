using BeautySalon.Entities.Commons;
using BeautySalon.Entities.WhyUsSections;

namespace BeautySalon.Test.Tool.Entities.WhyUsSections;
public class WhyUsSectionBuilder
{
    private readonly Why_Us_Section _section;

    public WhyUsSectionBuilder()
    {
        _section = new Why_Us_Section()
        {
            CreateDate = DateTime.Now,
            Image = new MediaDocument()
            {
                Extension = "extension",
                ImageName = "imageName",
                UniqueName = "uniqueName",
                URL = "url"
            },
            Title = "title",
            Description = "description",
        };
    }

    public WhyUsSectionBuilder WithTitle(string title)
    {
        _section.Title = title;
        return this;
    }
    public WhyUsSectionBuilder WithDescription(string description)
    {
        _section.Description = description;
        return this;
    }

    public WhyUsSectionBuilder WithMedia()
    {
        _section.Image = new MediaDocument()
        {
            Extension = "extension",
            ImageName = "imageName",
            UniqueName = "uniqueName",
            URL = "url"
        };
        return this;
    }

    public WhyUsSectionBuilder WithQuestion()
    {
        _section.Why_Us_Questions.Add(new Why_Us_Question()
        {
            Answer="answer",
            Question="question",
            CreateDate=DateTime.Now,
        });
        return this;
    }

    public Why_Us_Section Build()
    {
        return _section;
    }
}
