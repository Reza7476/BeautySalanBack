using BeautySalon.Services.Users.Contracts.Dtos;

namespace BeautySalon.Test.Tool.Entities.Users;
public class EditUserProfileDtoBuilder
{
    private readonly EditUserProfileDto _dto;

    public EditUserProfileDtoBuilder()
    {
        _dto = new EditUserProfileDto()
        {
            BirthDate = DateTime.Now,
            Email = "email",
            LastName = "lastName",
            Name = "name",
            UserName = "userName"
        };
    }

    public EditUserProfileDtoBuilder WithName(string name)
    {
        _dto.Name = name;
        return this;
    }

    public EditUserProfileDtoBuilder withLastName(string lastName)
    {
        _dto.LastName = lastName;
        return this;
    }

    public EditUserProfileDtoBuilder WithUserName(string userName)
    {
        _dto.UserName = userName;
        return this;
    }

    public EditUserProfileDtoBuilder WithEmail(string email)
    {
        _dto.Email= email;
       return this;
    }

    public EditUserProfileDto Build()
    {
        return _dto;
    }
}
