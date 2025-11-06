using BeautySalon.Common.Dtos;

namespace BeautySalon.Services.Clients.Contracts.Dtos;
public class GetAllClientsForAddAppointment
{
    public string MobileNumber { get; set; } = default!;
    public string Id { get; set; } = default!;
    public ImageDetailsDto? Profile { get; set; } 
    public string? Name { get; set; }
    public string? LastName { get; set; }
}
