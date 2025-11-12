using BeautySalon.Services.Users.Contracts.Dtos;

namespace BeautySalon.Test.Tool.Entities.Users;
public class ChangeUserActivationDtoBuilder
{
    private readonly ChangeUserActivationDto _dto;

    public ChangeUserActivationDtoBuilder()
    {
        _dto=new ChangeUserActivationDto()
        {
            IsActive = true,
        };
    }


    public ChangeUserActivationDtoBuilder WithUserId(string id)
    {
        _dto.UserId = id;   
        return this;    
    }

    public ChangeUserActivationDtoBuilder WithIcActive(bool isActive)
    {
        _dto.IsActive=isActive;
        return this;
    }

    public ChangeUserActivationDto Build()
    {
        return _dto;
    }

}
