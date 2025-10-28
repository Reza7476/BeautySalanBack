using BeautySalon.Services.Clients.Contracts.Dtos;

namespace BeautySalon.Test.Tool.Entities.Clients;
public class AddClientDtoBuilder
{
    private readonly AddClientDto _dto;

    public AddClientDtoBuilder()
    {
        _dto = new AddClientDto()
        {
            UserId=Guid.NewGuid().ToString(),
        };
    }

    public AddClientDtoBuilder WithUser(string id)
    {
        _dto.UserId=id;
        return this;    
    }

    public AddClientDto Build()
    {
        return _dto;
    }
}
