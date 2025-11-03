using BeautySalon.Services.Users.Contracts;
using BeautySalon.Test.Tool.Entities.Users;
using BeautySalon.Test.Tool.Infrastructure.Integration;
using FluentAssertions;
using Xunit;

namespace BeautySalon.Service.IntegrationTest.Users;
public class UserServiceTests : BusinessIntegrationTest
{
    private readonly IUserService _sut;

    public UserServiceTests()
    {
        _sut = UserServiceFactory.Generate(DbContext);
    }

    [Fact]
    public async Task GetUSerInfo_should_return_user_unfo_properly()
    {
        var user = new UserBuilder()
            .WithMobile("09174367476")
            .WithIsActive(true)
            .WithAvatar()
            .WithEmial("email")
            .WithLastName("dehghani")
            .WithName("Reza")
            .WithBirthDate(DateTime.Now)
            .WithUserName("username")
            .Build();
        Save(user);

        var expected = await _sut.GetUserInfo(user.Id);

        expected!.Avatar!.ImageName.Should().Be(user.Avatar!.ImageName);
        expected.Avatar.Extension.Should().Be(user.Avatar.Extension);
        expected.Avatar.UniqueName.Should().Be(user.Avatar.UniqueName);
        expected.Avatar.URL.Should().Be(user.Avatar.URL);
        expected.Email.Should().Be(user.Email);
        expected.Name.Should().Be(user.Name);
        expected.IsActive.Should().Be(true);
        expected.Mobile.Should().Be(user.Mobile);
        expected.UserName.Should().Be(user.UserName);
        expected.LastName.Should().Be(user.LastName);
    }
}
