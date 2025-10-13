using BeautySalon.Entities.Commons;
using BeautySalon.Entities.Users;

namespace BeautySalon.Service.UnitTest.Users;
public class UserBuilder
{
    private readonly User _user;


    public UserBuilder()
    {
        _user = new User()
        {
            Avatar = new MediaDocument()
            {
                Extension = ".jpg",
                ImageName = "name",
                UniqueName = "unique",
                URL = "url"

            },
            BirthDate = DateTime.Now,
            CreationDate = DateTime.Now,
            Email = "email",
            Id = Guid.NewGuid().ToString(),
            LastName = "lastName",
            Mobile = "09174367476",
            UserName = "userName",
        };
    }


    public UserBuilder WithName(string name)
    {
        _user.Name = name;
        return this;
    }

    public UserBuilder WithLastName(string lastName)
    {
        _user.LastName = lastName;
        return this;
    }

    public UserBuilder WithMobile(string mobile)
    {
        _user.Mobile = mobile;
        return this;
    }

    public UserBuilder WithEmial(string email)
    {
        _user.Email = email;
        return this;
    }

    public UserBuilder WithIsActive(bool isActive)
    {
        _user.IsActive = isActive;
        return this;
    }

    public UserBuilder WithBirthDate(DateTime birthDate)
    {
        _user.BirthDate = birthDate;
        return this;
    }

    public UserBuilder WithAvatar()
    {
        _user.Avatar = new MediaDocument()
        {
            Extension = ".jpg",
            ImageName = "name",
            UniqueName = "unique",
            URL = "url"

        };
        return this;
    }


    public UserBuilder WithUserName(string userName)
    {
        _user.UserName = userName;
        return this;
    }
    public User Build()
    {
        return _user;
    }
}
