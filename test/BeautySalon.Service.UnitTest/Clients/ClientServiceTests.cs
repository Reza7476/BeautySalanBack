using BeautySalon.Services.Clients.Contracts;
using BeautySalon.Test.Tool.Entities.Clients;
using BeautySalon.Test.Tool.Entities.Users;
using BeautySalon.Test.Tool.Infrastructure.Integration;
using FluentAssertions;
using Xunit;

namespace BeautySalon.Service.UnitTest.Clients;
public class ClientServiceTests : BusinessIntegrationTest
{
    private readonly IClientService _sut;

    public ClientServiceTests()
    {
        _sut = ClientServiceFactory.Generate(SetupContext);
    }

    [Fact]
    public async Task GetAllForAppointement_should_return_all_properly()
    {
        var user = new UserBuilder()
            .WithMobile("091274367476")
            .WithAvatar()
            .WithName("Reza")
            .WithLastName("Dehghani")
            .Build();
        Save(user);
        var client = new ClientBuilder()
            .WithUser(user.Id)
            .Build();
        Save(client);

        var expected = await _sut.GetAllForAppointment();

        expected.First().Name.Should().Be(user.Name);
        expected.First().LastName.Should().Be(user.LastName);
        expected.First().MobileNumber.Should().Be(user.Mobile);
        expected.First().Profile!.URL.Should().Be(user.Avatar!.URL);
        expected.First().Profile!.ImageName.Should().Be(user.Avatar.ImageName);
        expected.First().Profile!.Extension.Should().Be(user.Avatar.Extension);
        expected.First().Profile!.UniqueName.Should().Be(user.Avatar.UniqueName);
    }
}
