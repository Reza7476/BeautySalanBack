namespace BeautySalon.Services.Appointments.Contracts.Dtos;
public class GetBookedAppointmentByDayDto
{
    public TimeOnly StartDate { get; set; }
    public TimeOnly EndDate { get; set; }
    public int Duration { get; set; }
}
