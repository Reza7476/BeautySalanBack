using System.ComponentModel;

namespace BeautySalon.Services.Appointments.Contracts.Dtos;
public class GetAppointmentRequiringSMSDto
{
    public string TreatmentTitle { get; set; } = default!;
    public string AppointmentId { get; set; } = default!;
    public string? ClientNumber { get; set; } 
    public string? ClientName { get; set; }
    public string? ClientLastName { get; set; }
    public DateTime AppointmentData { get; set; }
}
