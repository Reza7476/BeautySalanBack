using BeautySalon.Entities.Clients;
using BeautySalon.Entities.Technicians;
using BeautySalon.Entities.Treatments;
using BeautySalon.Entities.WeeklySchedules;

namespace BeautySalon.Entities.Appointments;
public class Appointment
{
    public string Id { get; set; } = default!;
    public string ClientId { get; set; } = default!;
    public string TechnicianId { get; set; } = default!;
    public long TreatmentId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public DateTime EndTime { get; set; }
    public int Duration { get; set; }
    public AppointmentStatus Status { get; set; }
    public string? Description { get; set; }
    public string? CancelledBy { get; set; }
    public DateTime CancelledAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public Client Client { get; set; } = default!;
    public Treatment Treatment { get; set; } = default!;
    public Technician Technician { get; set; } = default!;
    public DayWeek DayWeek { get; set; }
}

public enum AppointmentStatus : byte
{
    None = 0,
    Completed = 1,
    Confirmed = 2,
    Pending = 3,
    NoShow = 4,
    Cancelled = 5,
}