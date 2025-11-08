using BeautySalon.Entities.Clients;
using BeautySalon.Services.Clients.Contracts;
using BeautySalon.Test.Tool.Entities.Clients;
using BeautySalon.Test.Tool.Entities.Users;
using BeautySalon.Test.Tool.Infrastructure.UnitTests;
using FluentAssertions;
using Xunit;

namespace BeautySalon.Service.UnitTest.Clients;
public class ClientServiceTests : BusinessUnitTest
{
    private readonly IClientService _sut;

    public ClientServiceTests()
    {
        _sut = ClientServiceFactory.Generate(SetupContext);
    }

    [Fact]
    public async Task Add_should_add_client_properly()
    {
        var user = new UserBuilder()
            .Build();
        Save(user);
        var dto = new AddClientDtoBuilder()
            .WithUser(user.Id)
            .Build();

        await _sut.Add(dto);

        var expected = ReadContext.Set<Client>().First();
        expected.UserId.Should().Be(user.Id);
    }
}
