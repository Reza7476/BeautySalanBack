using BeautySalon.Entities.Appointments;
using BeautySalon.Entities.WeeklySchedules;
using BeautySalon.Services.Appointments.Contracts;
using BeautySalon.Test.Tool.Entities.Appointments;
using BeautySalon.Test.Tool.Entities.Clients;
using BeautySalon.Test.Tool.Entities.Technicians;
using BeautySalon.Test.Tool.Entities.Treatments;
using BeautySalon.Test.Tool.Entities.Users;
using BeautySalon.Test.Tool.Infrastructure.Integration;
using FluentAssertions;
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

    [Fact]
    public async Task GetAdminAllAppointments_should_return_all_appointment_for_admin_properly()
    {
        var date = DateTime.Now;
        var user = new UserBuilder()
            .WithName("Reza")
            .WithLastName("dehghani")
            .WithMobile("09174367476")
            .Build();
        Save(user);
        var client = new ClientBuilder()
            .WithUser(user.Id)
            .Build();
        Save(client);
        var treatment = new TreatmentBuilder()
            .WithTitle("Nile")
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

        var expected = await _sut.GetAdminAllAppointments();

        expected.Elements.First().Id.Should().Be(appointment.Id);
        expected.Elements.First().TreatmentTitle.Should().Be(treatment.Title);
        expected.Elements.First().ClientName.Should().Be(user.Name);
        expected.Elements.First().ClientLastName.Should().Be(user.LastName);
        expected.Elements.First().ClientMobile.Should().Be(user.Mobile);
        expected.Elements.First().Duration.Should().Be(30);
        expected.Elements.First().StartTime.Should().Be(TimeOnly.FromDateTime(date.AddDays(1)));
        expected.Elements.First().EndTime.Should().Be(TimeOnly.FromDateTime(date.AddDays(1).AddMinutes(30)));
        expected.Elements.First().Status.Should().Be(appointment.Status);
        expected.Elements.First().DayWeek.Should().Be(appointment.DayWeek);
        expected.Elements.First().AppointmentDate.Should().Be(DateOnly.FromDateTime(appointment.AppointmentDate));
    }

    [Fact]
    public async Task GetDetails_should_return_appointment_details()
    {
        var date = DateTime.Now;
        var user = new UserBuilder()
            .WithName("Reza")
            .WithLastName("dehghani")
            .WithMobile("09174367476")
            .Build();
        Save(user);
        var client = new ClientBuilder()
            .WithUser(user.Id)
            .Build();
        Save(client);
        var treatment = new TreatmentBuilder()
            .WithTitle("Nile")
            .WithPrice(124)
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

        var expected = await _sut.GetDetails(appointment.Id);

        expected!.TreatmentTitle.Should().Be(treatment.Title);
        expected.ClientName.Should().Be(user.Name);
        expected.ClientLastName.Should().Be(user.LastName);
        expected.ClientMobile.Should().Be(user.Mobile);
        expected.Duration.Should().Be(30);
        expected.StartTime.Should().Be(TimeOnly.FromDateTime(date.AddDays(1)));
        expected.EndTime.Should().Be(TimeOnly.FromDateTime(date.AddDays(1).AddMinutes(30)));
        expected.Status.Should().Be(appointment.Status);
        expected.Day.Should().Be(appointment.DayWeek);
        expected.Date.Should().Be(DateOnly.FromDateTime(appointment.AppointmentDate));
        expected.Price.Should().Be(treatment.Price);
    }

    [Fact]
    public async Task GetAllToday_should_return_all_today_appointment()
    {
        var date = DateTime.Now;
        var user = new UserBuilder()
            .WithName("Reza")
            .WithLastName("dehghani")
            .WithMobile("09174367476")
            .Build();
        Save(user);
        var client = new ClientBuilder()
            .WithUser(user.Id)
            .Build();
        Save(client);
        var treatment = new TreatmentBuilder()
            .WithTitle("Nile")
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
            .WithAppointmentDate(date.AddHours(1))
            .WithEndTime(date.AddDays(1).AddMinutes(30))
            .WithDuration(30)
            .WithStatus(AppointmentStatus.Pending)
            .Build();
        Save(appointment);

        var expected = await _sut.GetAllToday();

        expected.Elements.First().Id.Should().Be(appointment.Id);
        expected.Elements.First().TreatmentTitle.Should().Be(treatment.Title);
        expected.Elements.First().ClientName.Should().Be(user.Name);
        expected.Elements.First().ClientLastName.Should().Be(user.LastName);
        expected.Elements.First().ClientMobile.Should().Be(user.Mobile);
        expected.Elements.First().Duration.Should().Be(30);
        //expected.Elements.First().StartTime.Should().Be(TimeOnly.FromDateTime(date.AddHours(1)));
        //expected.Elements.First().EndTime.Should().Be(TimeOnly.FromDateTime(date.AddHours(1).AddMinutes(30)));
        expected.Elements.First().Status.Should().Be(appointment.Status);
        expected.Elements.First().DayWeek.Should().Be(appointment.DayWeek);
        expected.Elements.First().AppointmentDate.Should().Be(DateOnly.FromDateTime(appointment.AppointmentDate));
    }

    [Fact]
    public async Task GetAppointmentPerDayForChart_should_return_appointment_each_day_properly()
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
            .WithAppointmentDate(date)
            .WithDayWeek(DayWeek.Saturday)
            .WithEndTime(date.AddMinutes(30))
            .Build();
        Save(appointment);

        var expected = await _sut.GetAppointmentPerDayForChart();

        expected.Count.Should().Be(7);
        expected.First().DayWeek.Should().Be(appointment.DayWeek);
        expected.First().Count.Should().Be(1);
    }

    [Fact] 
    public async Task should_return_summary_for_admin_dashboard()
    {
        var user = new UserBuilder()
            .Build();
        Save(user);
        var client = new ClientBuilder()
            .WithUser(user.Id)
            .Build();
        Save(client);
        var technician=new TechnicianBuilder()
            .WithUser(user.Id)
            .Build();
        Save(technician);
        var treatment = new TreatmentBuilder()
            .Build();
        Save(treatment);
        var  appointment = new AppointmentBuilder()
            .WithAppointmentDate(DateTime.Now)
            .WithClient(client.Id)
            .WithTechnicianId(technician.Id)
            .WithTreatment(treatment.Id)
            .Build();
        Save(appointment);

        var expected = await _sut.GetAdminDashboardSummary();

        expected!.TodayAppointments.Should().Be(1);
        expected.TotalClients.Should().Be(1);
        expected.TotalTreatments.Should().Be(1);
    }

    [Fact]
    public async Task GetNewAppointmentDashboard_should_return_new_appointment_properly()
    {
        var date= DateTime.Now;
        var user = new UserBuilder()
            .WithName("Reza")
            .WithLastName("Dehghani")
            .WithMobile("9174367476")
           .Build();
        Save(user);
        var client = new ClientBuilder()
            .WithUser(user.Id)
            .Build();
        Save(client);
        var technician = new TechnicianBuilder()
            .WithUser(user.Id)
            .Build();
        Save(technician);
        var treatment = new TreatmentBuilder()
            .WithTitle("Title")
            .Build();
        Save(treatment);
        var appointment = new AppointmentBuilder()
            .WithAppointmentDate(date)
            .WithClient(client.Id)
            .WithTechnicianId(technician.Id)
            .WithTreatment(treatment.Id)
            .WithDayWeek(DayWeek.Saturday)
            .Build();
        Save(appointment);

        var expected = await _sut.GetNewAppointmentDashboard();

        expected.First().Mobile.Should().Be(user.Mobile);
        expected.First().ClientName.Should().Be(user.Name);
        expected.First().ClientLastName.Should().Be(user.LastName);
        expected.First().Date.Should().Be(DateOnly.FromDateTime(date));
        expected.First().Status.Should().Be(appointment.Status);
        expected.First().DayWeek.Should().Be(appointment.DayWeek);
        expected.First().TreatmentTitle.Should().Be(treatment.Title);
    }
}