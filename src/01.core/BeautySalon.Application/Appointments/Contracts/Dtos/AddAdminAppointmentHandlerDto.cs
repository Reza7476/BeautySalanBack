using BeautySalon.Entities.WeeklySchedules;
using System.ComponentModel.DataAnnotations;

namespace BeautySalon.Application.Appointments.Contracts.Dtos;
public class AddAdminAppointmentHandlerDto
{
    [Required]
    public long TreatmentId { get; set; }

    [Required]
    public DateTime AppointmentDate { get; set; }

    [Required]
    public int Duration { get; set; }

    public DayWeek DayWeek { get; set; }

    [Required]
    public string ClientId { get; set; } = default!;
}
