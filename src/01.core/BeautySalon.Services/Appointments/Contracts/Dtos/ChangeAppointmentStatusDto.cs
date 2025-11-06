using BeautySalon.Entities.Appointments;

namespace BeautySalon.Services.Appointments.Contracts.Dtos;
public class ChangeAppointmentStatusDto
{
    public string Id { get; set; } = default!;
    public AppointmentStatus Status { get; set; }
}
