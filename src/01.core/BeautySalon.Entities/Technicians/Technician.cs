using BeautySalon.Entities.Appointments;

namespace BeautySalon.Entities.Technicians;
public class Technician
{
    public Technician()
    {
        Appointments = new HashSet<Appointment>();
    }

    public string Id { get; set; } = default!;
    public string UserId { get; set; } = default!;
    public HashSet<Appointment> Appointments { get; set; }
}
