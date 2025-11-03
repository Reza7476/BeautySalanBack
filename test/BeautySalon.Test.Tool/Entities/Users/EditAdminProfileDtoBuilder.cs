using BeautySalon.Services.Users.Contracts.Dtos;

namespace BeautySalon.Test.Tool.Entities.Users;
public class EditAdminProfileDtoBuilder
{
    private readonly EditAdminProfileDto _dto;

    public EditAdminProfileDtoBuilder()
    {
        _dto = new EditAdminProfileDto()
        {
            BirthDate = DateTime.Now,
            Email = "email",
            LastName = "lastName",
            Name = "name",
        };
    }

    public EditAdminProfileDtoBuilder WithName(string name)
    {
        _dto.Name = name;
        return this;
    }

    public EditAdminProfileDtoBuilder withLastName(string lastName)
    {
        _dto.LastName = lastName;
        return this;
    }

    public EditAdminProfileDtoBuilder WithEmail(string email)
    {
        _dto.Email= email;
       return this;
    }

    public EditAdminProfileDto Build()
    {
        return _dto;
    }
}
