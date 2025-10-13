using BeautySalon.Services.Users.Dtos;

namespace BeautySalon.Test.Tool.Entities.Users;
public class AddUserDtoBuilder
{
    private readonly AddUserDto _dto;

    public AddUserDtoBuilder()
    {
        _dto = new AddUserDto()
        {
            Email = "email.com",
            LastName = "lastName",
            Mobile = "mobile",
            Name = "name",
            Password = " ",
            UserName = " "
        };
    }

    public AddUserDtoBuilder WithName(string name)
    {
        _dto.Name = name;
        return this;
    }

    public AddUserDtoBuilder WithLastName(string lastName)
    {
        _dto.LastName = lastName;
        return this;
    }

    public AddUserDtoBuilder WithEmail(string email)
    {
        _dto.Email = email;
        return this;
    }

    public AddUserDtoBuilder WithMobile(string mobile)
    {
        _dto.Mobile = mobile;
        return this;
    }

    public AddUserDtoBuilder WithPassword(string password)
    {
        _dto.Password = password;
        return this;
    }

    public AddUserDtoBuilder WithUserName(string userName)
    {
        _dto.UserName = userName;
        return this;
    }


    public AddUserDto Build()
    {
        return _dto;
    }
}
