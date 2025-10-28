using BeautySalon.Services.Technicians.Contracts.Dtos;

namespace BeautySalon.Test.Tool.Entities.Technicians;
public class AddTechnicianDtoBuilder
{
    private readonly AddTechnicianDto _dto;


    public AddTechnicianDtoBuilder()
    {
        _dto = new AddTechnicianDto()
        {
            UserId=Guid.NewGuid().ToString(),
        };
    }

    public AddTechnicianDtoBuilder WithUser(string id)
    {
        _dto.UserId = id;
        return this;
    }

    public AddTechnicianDto Build()
    {
        return _dto;
    }

}
