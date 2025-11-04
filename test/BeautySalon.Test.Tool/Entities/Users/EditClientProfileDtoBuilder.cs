using BeautySalon.Services.Users.Contracts.Dtos;

namespace BeautySalon.Test.Tool.Entities.Users;
public class EditClientProfileDtoBuilder
{
    private readonly EditClientProfileDto _dto;

    public EditClientProfileDtoBuilder()
    {
        _dto = new EditClientProfileDto()
        {
            BirthDateGregorian = DateTime.Now,
            Email = "email",
            LastName = "lastName",
            Name = "name",
            UserName = "Reza"
        };
    }

    public EditClientProfileDtoBuilder WithName(string name)
    {
        _dto.Name = name;
        return this;
    }

    public EditClientProfileDtoBuilder WithLastName(string lastName)
    {
        _dto.LastName = lastName;
        return this;
    }

    public EditClientProfileDtoBuilder WithUserName(string userName)
    {
        _dto.UserName = userName;
        return this;
    }

    public EditClientProfileDtoBuilder WithBirthDate(DateTime time)
    {
        _dto.BirthDateGregorian = time;
        return this;
    }
    public EditClientProfileDtoBuilder WithEmail(string email)
    {
        _dto.Email = email;
        return this;
    }

    public EditClientProfileDto Build()
    {
        return _dto;
    }
}
