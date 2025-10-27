using BeautySalon.Entities.Treatments;

namespace BeautySalon.Test.Tool.Entities.Treatments;
public class TreatmentBuilder
{
    private readonly Treatment _treatment;

    public TreatmentBuilder()
    {
        _treatment = new Treatment()
        {
            CreateDate = DateTime.Now,
            Description = "description",
            Title = "title",
            Duration=180,
            Images = new HashSet<TreatmentImage>()
        };
    }
    
    public TreatmentBuilder WithDuration(int duration)
    {
        _treatment.Duration= duration;
        return this;
    }

    public TreatmentBuilder WithTitle(string title)
    {
        _treatment.Title = title;
        return this;
    }

    public TreatmentBuilder WithDescription(string description)
    {
        _treatment.Description = description;
        return this;
    }

    public TreatmentBuilder WithImage()
    {
        _treatment.Images.Add(new TreatmentImage()
        {
            CreateDate = DateTime.Now,
            ImageName = "imageName",
            ImageUniqueName = "imageUniqueName",
            URL = "url",
            Extension="extension"
        });
        return this;
    }

    public Treatment Build()
    {
        return _treatment;
    }
}
