using System.ComponentModel.DataAnnotations;

namespace BeautySalon.Application.Appointments.Contracts.Dtos;
public class AddAppointmentHandlerDto
{

    [Required]
    public long TreatmentId { get; set; }

    [Required]
    public DateTime AppointmentDate { get; set; }

    [Required]
    public DateTime EndTime { get; set; }

    [Required]
    public int Duration { get; set; }

    public string? Description { get; set; }
}
