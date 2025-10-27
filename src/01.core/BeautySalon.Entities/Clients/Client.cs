using BeautySalon.Entities.Appointments;

namespace BeautySalon.Entities.Clients;
public class Client
{
    public Client()
    {
        Appointments = new HashSet<Appointment>();
    }
    public string Id { get; set; } = default!;
    public string UserId { get; set; } = default!;
    public HashSet<Appointment> Appointments { get; set; }
}
