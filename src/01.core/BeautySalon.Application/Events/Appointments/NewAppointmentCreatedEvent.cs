namespace BeautySalon.Application.Events.Appointments;
public class NewAppointmentCreatedEvent
{
    public string AppointmentId { get; }
    public DateTime CreatedAt { get; }

    public NewAppointmentCreatedEvent(string appointmentId)
    {
        AppointmentId = appointmentId;
        CreatedAt = DateTime.UtcNow;
    }
}