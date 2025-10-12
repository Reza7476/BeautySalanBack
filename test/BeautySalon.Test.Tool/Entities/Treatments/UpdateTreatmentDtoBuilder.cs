using BeautySalon.Services.Treatments.Contracts.Dto;

namespace BeautySalon.Test.Tool.Entities.Treatments;
public class UpdateTreatmentDtoBuilder
{
    private readonly UpdateTreatmentDto _dto;

    public UpdateTreatmentDtoBuilder()
    {
        _dto = new UpdateTreatmentDto()
        {
            Description = "description",
            Title = "title"
        };
    }
    public UpdateTreatmentDtoBuilder WithTitle(string title)
    {
        _dto.Title = title;
        return this;
    }

    public UpdateTreatmentDtoBuilder WithDescription(string description)
    {
        _dto.Description=description;
        return this;    
    }

    public UpdateTreatmentDto Build()
    {
        return _dto;
    }
}
