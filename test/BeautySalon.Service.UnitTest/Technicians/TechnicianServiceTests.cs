using BeautySalon.Entities.Technicians;
using BeautySalon.Services.Technicians.Contracts;
using BeautySalon.Test.Tool.Entities.Technicians;
using BeautySalon.Test.Tool.Entities.Users;
using BeautySalon.Test.Tool.Infrastructure.UnitTests;
using FluentAssertions;
using Xunit;

namespace BeautySalon.Service.UnitTest.Technicians;
public class TechnicianServiceTests : BusinessUnitTest
{
    private readonly ITechnicianService _sut;

    public TechnicianServiceTests()
    {
        _sut = TechnicianServiceFactory.Generate(DbContext);
    }

    [Fact]
    public async Task Add_should_add_technician_properly()
    {
        var user = new UserBuilder()
            .Build();
        Save(user);

        var dto = new AddTechnicianDtoBuilder()
            .WithUser(user.Id)
            .Build();

        await _sut.Add(dto);

        var expected = ReadContext.Set<Technician>().First();
        expected.UserId.Should().Be(dto.UserId);
    }
}
