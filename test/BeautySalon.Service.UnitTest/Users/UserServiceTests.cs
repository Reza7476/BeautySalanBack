using BeautySalon.Entities.Users;
using BeautySalon.Services.Clients.Exceptions;
using BeautySalon.Services.Users.Contracts;
using BeautySalon.Services.Users.Contracts.Dtos;
using BeautySalon.Services.Users.Exceptions;
using BeautySalon.Test.Tool.Entities.Users;
using BeautySalon.Test.Tool.Infrastructure.UnitTests;
using FluentAssertions;
using Xunit;

namespace BeautySalon.Service.UnitTest.Users;
public class UserServiceTests : BusinessUnitTest
{
    private readonly IUserService _sut;
    public UserServiceTests()
    {
        _sut = UserServiceFactory.Generate(DbContext);
    }

    [Fact]
    public async Task Add_should_add_userProperly()
    {
        var dto = new AddUserDtoBuilder()
            .WithMobile("09174367476")
            .WithLastName("dehghani")
            .WithName("name")
            .WithEmail("email.com")
            .WithPassword("dd")
            .WithUserName("user")
            .Build();

        await _sut.Add(dto);

        var expected = ReadContext.Set<User>().First();
        expected.Name.Should().Be(dto.Name);
        expected.LastName.Should().Be(dto.LastName);
        expected.Email.Should().Be(dto.Email);
        expected.UserName.Should().Be(dto.UserName);
        expected.Mobile.Should().Be(dto.Mobile);
        BCrypt.Net.BCrypt.Verify(dto.Password, expected.HashPass).Should().BeTrue();
    }

    [Fact]
    public async Task Add_should_throw_exception_when_mobile_is_exist()
    {
        var user = new UserBuilder()
            .WithMobile("+989174367476")
            .Build();
        Save(user);
        var dto = new AddUserDtoBuilder()
            .WithMobile("+989174367476")
            .Build();

        Func<Task> expected = async () => await _sut.Add(dto);

        await expected.Should().ThrowExactlyAsync<MobileNumberIsDuplicateException>();
    }

    [Fact]
    public async Task Add_should_throw_exception_when_user_name_is_duplicate()
    {
        var user = new UserBuilder()
            .WithUserName("reza")
            .Build();
        Save(user);
        var dto = new AddUserDtoBuilder()
            .WithUserName("reza")
            .Build();

        Func<Task> expected = async () => await _sut.Add(dto);

        await expected.Should().ThrowExactlyAsync<UserNameIsDuplicateException>();
    }

    [Fact]
    public async Task EditAdminProfile_should_update_profile_properly()
    {
        var user = new UserBuilder()
            .WithEmial("email")
            .WithUserName("Reza")
            .WithName("Reza")
            .WithLastName("Dehghani")
            .Build();
        Save(user);
        var dto = new EditAdminProfileDtoBuilder()
            .WithName("name")
            .withLastName("lastName")
            .WithEmail("email")
            .Build();

        await _sut.EditAdminProfile(dto, user.Id);

        var expected = ReadContext.Set<User>().First();
        expected.Name.Should().Be(dto.Name);
        expected.LastName.Should().Be(dto.LastName);
        expected.Email.Should().Be(dto.Email);
        
    }

    [Fact]
    public async Task EditAdminProfile_should_throw_exception_when_user_Id_is_null()
    {
        var dto = new EditAdminProfileDtoBuilder()
            .Build();
        Func<Task> expected = async () => await _sut.EditAdminProfile(dto, null);
        await expected.Should().ThrowAsync<YouAreNotAllowedToAccessException>();
    }


    //[Fact]
    //public async Task EditProfile_should_throw_exception_when_user_name_is_duplicate()
    //{
    //    var user1 = new UserBuilder()
    //        .WithUserName("userName")
    //        .Build();
    //    Save(user1);
    //    var user2 = new UserBuilder()
    //        .WithUserName("userName")
    //        .Build();
    //    Save(user2);
    //    var dto = new EditUserProfileDtoBuilder()
    //        .WithUserName("userName")
    //        .Build();
    //    Func<Task> expected = async () => await _sut.EditAdminProfile(dto, user1.Id);

    //    await expected.Should().ThrowAsync<UserNameIsDuplicateException>();
    //}
}
