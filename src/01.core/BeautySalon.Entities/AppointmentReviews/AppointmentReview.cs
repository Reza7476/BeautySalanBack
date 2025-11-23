using BeautySalon.Entities.Appointments;

namespace BeautySalon.Entities.AppointmentReviews;
public class AppointmentReview
{
    public string Id { get; set; } = default!;
    public string AppointmentId { get; set; } = default!;
    public string ClientId { get; set; } = default!;
    public long TreatmentId { get; set; }
    public string TechnicianId { get; set; } = default!;
    public byte Rate { get; set; } // 1 to 5
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsPublished { get; set; }
    public Appointment Appointment { get; set; } = default!;
}
