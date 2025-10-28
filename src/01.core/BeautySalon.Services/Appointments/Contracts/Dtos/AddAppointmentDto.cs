using BeautySalon.Entities.Appointments;
using System.ComponentModel.DataAnnotations;

namespace BeautySalon.Services.Appointments.Contracts.Dtos;
public class AddAppointmentDto
{
    public string ClientId { get; set; } = default!;
    public string TechnicianId { get; set; } = default!;
    public long TreatmentId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public int Duration { get; set; }
}
