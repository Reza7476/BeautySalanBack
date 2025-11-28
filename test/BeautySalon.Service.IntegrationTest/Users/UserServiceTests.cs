using BeautySalon.Services.Users.Contracts;
using BeautySalon.Test.Tool.Entities.Appointments;
using BeautySalon.Test.Tool.Entities.Clients;
using BeautySalon.Test.Tool.Entities.Roles;
using BeautySalon.Test.Tool.Entities.Technicians;
using BeautySalon.Test.Tool.Entities.Treatments;
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
    [Fact]
    public async Task GetAllUsers_should_return_all_users_properly()
    {
        var user = new UserBuilder()
            .WithName("Reza")
            .WithLastName("Dehghani")
            .WithMobile("91743674756")
            .WithAvatar()
            .WithEmial("Email")
            .WithUserName("UserName")
            .WithIsActive(true)
            .Build();
        Save(user);
        var role = new RoleBuilder()
            .WithName("role")
            .Build();
        Save(role);
        var userRole = new UserRoleBuilder()
            .WithRole(role.Id)
            .WithUser(user.Id)
            .Build();
        Save(userRole);
        var client = new ClientBuilder()
            .WithUser(user.Id)
            .Build();
        Save(client);
        var technician = new TechnicianBuilder()
            .WithUser(user.Id)
            .Build();
        Save(technician);
        var treatment = new TreatmentBuilder()
            .Build();
        Save(treatment);
        var appointment = new AppointmentBuilder()
            .WithClient(client.Id)
            .WithTechnicianId(technician.Id)
            .WithTreatment(treatment.Id)
            .Build();
        Save(appointment);

        var expected = await _sut.GetAllUsers();

        expected.Elements.First().Name.Should().Be(user.Name);
        expected.Elements.First().LastName.Should().Be(user.LastName);
        expected.Elements.First().Mobile.Should().Be(user.Mobile);
        expected.Elements.First().UserName.Should().Be(user.UserName);
        expected.Elements.First().Email.Should().Be(user.Email);
        expected.Elements.First().IsActive.Should().Be(user.IsActive);
        expected.Elements.First().CreatedAt.Should().Be(user.CreationDate);
        expected.Elements.First().Avatar!.Extension.Should().Be(user.Avatar!.Extension);
        expected.Elements.First().Avatar!.ImageName.Should().Be(user.Avatar.ImageName);
        expected.Elements.First().Avatar!.UniqueName.Should().Be(user.Avatar.UniqueName);
        expected.Elements.First().Avatar!.URL.Should().Be(user.Avatar.URL);
        expected.Elements.First().Roles.First().Should().Be(role.RoleName);
        expected.Elements.First().AppointmentNumber.Should().Be(1);

    }
}
