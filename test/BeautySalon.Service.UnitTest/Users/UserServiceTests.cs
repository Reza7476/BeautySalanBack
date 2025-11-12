using BeautySalon.Entities.Users;
using BeautySalon.Services.Clients.Exceptions;
using BeautySalon.Services.Users.Contracts;
using BeautySalon.Services.Users.Contracts.Dtos;
using BeautySalon.Services.Users.Exceptions;
using BeautySalon.Test.Tool.Common;
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


    [Fact]
    public async Task EditImageProfile_should_update_profile_properly()
    {
        var user1 = new UserBuilder()
            .WithAvatar()
            .Build();
        Save(user1);
        var dto = new ImageDetailsDtoBuilder()
           .Build();

        await _sut.EditImageProfile(dto, user1.Id);

        var expected = ReadContext.Set<User>().First();
        expected.Avatar!.Extension.Should().Be(dto.Extension);
        expected.Avatar.ImageName.Should().Be(dto.ImageName);
        expected.Avatar.UniqueName.Should().Be(dto.UniqueName);
        expected.Avatar.URL.Should().Be(dto.URL);
    }

    [Theory]
    [InlineData("userId")]
    public async Task EditImageProfile_should_throw_exception_when_user_not_found(string userId)
    {
        var dto = new ImageDetailsDtoBuilder()
            .Build();
        Func<Task> expected = async () => await _sut.EditImageProfile(dto, userId);
        await expected.Should().ThrowAsync<UserNotFoundException>();
    }


    [Fact]
    public async Task EditClientProfile_should_update_client_profile()
    {
        var user = new UserBuilder()
            .WithName("reza")
            .WithLastName("dehghani")
            .WithBirthDate(DateTime.Now)
            .WithEmial("email")
            .WithUserName("reza")
            .Build();
        Save(user);
        var dto = new EditClientProfileDtoBuilder()
            .WithName("Reza")
            .WithLastName("Dehghani")
            .WithUserName("UserName")
            .WithEmail("Email")
            .Build();

        await _sut.EditClientProfile(dto, user.Id);

        var expected = ReadContext.Set<User>().First();
        expected.Name.Should().Be(dto.Name);
        expected.LastName.Should().Be(dto.LastName);
        expected.UserName.Should().Be(dto.UserName);
        expected.Email.Should().Be(dto.Email);
    }

    [Fact]
    public async Task ChangeUserActivation_should_change_user_activity()
    {
        var admin = new UserBuilder()
            .WithMobile("9174367472")
            .Build();
        Save(admin);
        var client = new UserBuilder()
            .WithIsActive(true)
            .Build();
        Save(client);
        var dto = new ChangeUserActivationDtoBuilder()
            .WithIcActive(false)
            .WithUserId(client.Id)
            .Build();
        await _sut.ChangeUserActivation(dto, admin.Id);

        var expected = ReadContext.Set<User>().First();
        expected.IsActive.Should().Be(dto.IsActive);
    }


    [Theory]
    [InlineData("userId")]
    public async Task ChangeUserActivation_should_throw_exception_when_user_not_found(string userId)
    {
        var dto = new ChangeUserActivationDtoBuilder()
            .WithUserId(userId)
            .Build();
        Func<Task> expected = async () => await _sut.ChangeUserActivation(dto, userId);
        await expected.Should().ThrowAsync<UserNotFoundException>();
    }

    [Fact]
    public async Task ChangeUserActivation_should_throw_exception_when_user_is_admin()
    {
        var admin = new UserBuilder()
            .Build();
        Save(admin);
        var dto = new ChangeUserActivationDtoBuilder()
            .WithIcActive(false)
            .WithUserId(admin.Id)
            .Build();
        Func<Task> expected = async () => await _sut.ChangeUserActivation(dto, admin.Id);
        await expected.Should().ThrowAsync<YouAreNotAllowedToAccessException>();
    }

}
