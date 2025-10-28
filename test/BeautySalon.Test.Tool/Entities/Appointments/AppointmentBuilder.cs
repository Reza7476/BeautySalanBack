using BeautySalon.Entities.Appointments;
using System.Reflection.Metadata.Ecma335;

namespace BeautySalon.Test.Tool.Entities.Appointments;
public class AppointmentBuilder
{
    private readonly Appointment _appointment;

    public AppointmentBuilder()
    {
        _appointment = new Appointment()
        {
            Id = Guid.NewGuid().ToString(),
            AppointmentDate = DateTime.Now,
            Description = string.Empty,
            CreatedAt = DateTime.Now,
            Duration = 30,
            EndTime = DateTime.Now.AddMinutes(30),
            Status = AppointmentStatus.Pending,
        };
    }


    public AppointmentBuilder WithClient(string id)
    {
        _appointment.ClientId = id;
        return this;
    }

    public AppointmentBuilder WithTechnicianId(string id)
    {
        _appointment.TechnicianId = id;
        return this;
    }

    public AppointmentBuilder WithTreatment(long id)
    {
        _appointment.TreatmentId = id;
        return this;
    }

    public AppointmentBuilder WithAppointmentDate(DateTime time)
    {
        _appointment.AppointmentDate = time;
        return this;
    }

    public AppointmentBuilder WithEndTime(DateTime time)
    {
        _appointment.EndTime = time;
        return this;
    }

    public AppointmentBuilder WithDuration(int number)
    {
        _appointment.Duration = number;
        return this;
    }

    public AppointmentBuilder WithStatus(AppointmentStatus status)
    {
        _appointment.Status = status;
        return this;
    }


    public Appointment Build()
    {
        return _appointment;
    }

}


