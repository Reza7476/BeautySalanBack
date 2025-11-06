using BeautySalon.Entities.Appointments;
using BeautySalon.Services;
using BeautySalon.Services.Appointments.Contracts;
using BeautySalon.Services.Appointments.Exceptions;
using BeautySalon.Test.Tool.Entities.Appointments;
using BeautySalon.Test.Tool.Entities.Clients;
using BeautySalon.Test.Tool.Entities.Technicians;
using BeautySalon.Test.Tool.Entities.Treatments;
using BeautySalon.Test.Tool.Entities.Users;
using BeautySalon.Test.Tool.Infrastructure.UnitTests;
using FluentAssertions;
using Xunit;

namespace BeautySalon.Service.UnitTest.Appointments;
public class AppointmentServiceTests : BusinessUnitTest
{
    private readonly IAppointmentService _sut;

    public AppointmentServiceTests()
    {
        _sut = AppointmentServiceFactory.Generate(DbContext);
    }

    [Fact]
    public async Task Add_should_add_appointment_properly()
    {
        var userClient = new UserBuilder()
            .Build();
        Save(userClient);
        var client = new ClientBuilder()
            .WithUser(userClient.Id)
            .Build();
        Save(client);
        var userTechnician = new UserBuilder()
            .Build();
        Save(userTechnician);

        var technician = new TechnicianBuilder()
            .WithUser(userTechnician.Id)
            .Build();
        Save(technician);
        var treatment = new TreatmentBuilder()
            .Build();
        Save(treatment);

        var dto = new AddAppointmentDtoBuilder()
            .WithClientId(client.Id)
            .WithTechnicianId(technician.Id)
            .WithTreatmentId(treatment.Id)
            .WithDuration(30)
            .WithDateTime(DateTime.Now)
            .Build();

        await _sut.Add(dto);

        var expected = ReadContext.Set<Appointment>().First();
        expected.ClientId.Should().Be(client.Id);
        expected.TechnicianId.Should().Be(technician.Id);
        expected.TreatmentId.Should().Be(treatment.Id);
        expected.AppointmentDate.Should().Be(dto.AppointmentDate);
        // expected.EndTime.Should().Be(dto.AppointmentDate.AddMinutes(dto.Duration));
        expected.Duration.Should().Be(dto.Duration);
        expected.Status.Should().Be(AppointmentStatus.Pending);
    }


    [Fact]
    public async Task Add_should_throw_exception_when_appointment_is_busy()
    {
        var time = DateTime.Now;
        var userClient = new UserBuilder()
            .Build();
        Save(userClient);
        var client = new ClientBuilder()
            .WithUser(userClient.Id)
            .Build();
        Save(client);
        var userTechnician = new UserBuilder()
            .Build();
        Save(userTechnician);
        var technician = new TechnicianBuilder()
            .WithUser(userTechnician.Id)
            .Build();
        Save(technician);
        var treatment = new TreatmentBuilder()
            .Build();
        Save(treatment);
        var appointment = new AppointmentBuilder()
            .WithClient(client.Id)
            .WithTechnicianId(technician.Id)
            .WithTreatment(treatment.Id)
            .WithAppointmentDate(time)
            .WithDuration(30)
            .WithEndTime(time.AddMinutes(30))
            .WithStatus(AppointmentStatus.Pending)
            .Build();
        Save(appointment);

        var dto = new AddAppointmentDtoBuilder()
            .WithClientId(client.Id)
            .WithTechnicianId(technician.Id)
            .WithTreatmentId(treatment.Id)
            .WithDuration(30)
            .WithDateTime(time.AddMinutes(29))
            .Build();

        Func<Task> expected = async () => await _sut.Add(dto);

        await expected.Should().ThrowAsync<AppointmentIsBusyAtThisTimeException>();
    }

    [Fact]
    public async Task CancelByClient_should_cancel_appointment_properly()
    {
        var user = new UserBuilder()
            .Build();
        Save(user);
        var technician = new TechnicianBuilder()
            .WithUser(user.Id)
            .Build();
        Save(technician);
        var client = new ClientBuilder()
            .WithUser(user.Id)
            .Build();
        Save(client);
        var treatment = new TreatmentBuilder()
            .Build();
        Save(treatment);
        var appointment = new AppointmentBuilder()
            .WithClient(client.Id)
            .WithTechnicianId(technician.Id)
            .WithTreatment(treatment.Id)
            .Build();
        Save(appointment);

        await _sut.CancelByClient(appointment.Id, client.Id);

        var expected = ReadContext.Set<Appointment>().First();
        expected.Status.Should().Be(AppointmentStatus.Cancelled);
        expected.CancelledBy.Should().Be(SystemRole.Client);
    }


    [Theory]
    [InlineData("appointmentId", "clientId")]
    public async Task CancelByClient_should_throw_exception(string clientId, string appointmentId)
    {
        Func<Task> expected = async () => await _sut.CancelByClient(clientId, appointmentId);
        await expected.Should().ThrowAsync<AppointmentNotFoundException>();
    }

    [Theory]
    [InlineData("appointmentId")]
    public async Task ChangeStatus_should_throw_exception_when_appointment_not_found(string id)
    {
        var dto = new ChangeAppointmentStatusDtoBuilder()
            .WithId(id)
            .Build();

        Func<Task> expected = async () => await _sut.ChangeStatus(dto);

        await expected.Should().ThrowAsync<AppointmentNotFoundException>();
    }

    [Fact]
    public async Task ChangeStatus_should_change_appointment_status_properly()
    {
        var user = new UserBuilder()
            .Build();
        Save(user);
        var technician = new TechnicianBuilder()
            .WithUser(user.Id)
            .Build();
        Save(technician);
        var client = new ClientBuilder()
            .WithUser(user.Id)
            .Build();
        Save(client);
        var treatment = new TreatmentBuilder()
            .Build();
        Save(treatment);
        var appointment = new AppointmentBuilder()
            .WithClient(client.Id)
            .WithTechnicianId(technician.Id)
            .WithTreatment(treatment.Id)
            .WithStatus(AppointmentStatus.Pending)
            .Build();
        Save(appointment);
        var dto = new ChangeAppointmentStatusDtoBuilder()
            .WithId(appointment.Id)
            .WithStatus(AppointmentStatus.Confirmed)
            .Build();

        await _sut.ChangeStatus(dto);

        var expected = ReadContext.Set<Appointment>().First();
        expected.Status.Should().Be(dto.Status);
    }
}
