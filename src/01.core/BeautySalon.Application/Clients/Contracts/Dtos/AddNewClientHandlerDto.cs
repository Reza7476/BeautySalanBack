using System.ComponentModel.DataAnnotations;

namespace BeautySalon.Application.Clients.Contracts.Dtos;
public class AddNewClientHandlerDto
{

    [Required]
    public string Name { get; set; } = default!;

    [Required]
    public string LastName { get; set; }=default!;

    [Required]
    public string Mobile { get; set; } = default!;
}
