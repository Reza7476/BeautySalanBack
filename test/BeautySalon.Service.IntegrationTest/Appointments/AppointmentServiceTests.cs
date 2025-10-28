using BeautySalon.Entities.Appointments;
using BeautySalon.Services.Appointments.Contracts;
using BeautySalon.Test.Tool.Entities.Appointments;
using BeautySalon.Test.Tool.Entities.Clients;
using BeautySalon.Test.Tool.Entities.Technicians;
using BeautySalon.Test.Tool.Entities.Treatments;
using BeautySalon.Test.Tool.Entities.Users;
using BeautySalon.Test.Tool.Infrastructure.Integration;
using FluentAssertions;
using System.Security.Cryptography;
using Xunit;

namespace BeautySalon.Service.IntegrationTest.Appointments;
public class AppointmentServiceTests : BusinessIntegrationTest
{
    private readonly IAppointmentService _sut;

    public AppointmentServiceTests()
    {
        _sut = AppointmentServiceFactory.Generate(DbContext);
    }

    [Fact]
    public async Task GetBookedAppointmentByDay_should_return_booked_appointment_properly()
    {
        var date = DateTime.Now;
        var user = new UserBuilder()
            .Build();
        Save(user);
        var client = new ClientBuilder()
            .WithUser(user.Id)
            .Build();
        Save(client);
        var treatment = new TreatmentBuilder()
            .Build();
        Save(treatment);
        var technician = new TechnicianBuilder()
            .WithUser(user.Id)
            .Build();
        Save(technician);
        var appointment = new AppointmentBuilder()
            .WithClient(client.Id)
            .WithTechnicianId(technician.Id)
            .WithTreatment(treatment.Id)
            .WithAppointmentDate(date.AddDays(1))
            .WithEndTime(date.AddDays(1).AddMinutes(30))
            .WithDuration(30)
            .WithStatus(AppointmentStatus.Pending)
            .Build();
        Save(appointment);

        var expected = await _sut.GetBookAppointmentByDay(date.AddDays(1));

        expected.First().StartDate.Should().Be(TimeOnly.FromDateTime(date.AddDays(1)));
        expected.First().EndDate.Should().Be(TimeOnly.FromDateTime(date.AddDays(1).AddMinutes(30)));
        expected.First().Duration.Should().Be(30);
    }
}
