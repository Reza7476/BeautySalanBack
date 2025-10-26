using BeautySalon.Common.Dtos;

namespace BeautySalon.Services.Treatments.Contracts.Dto;
public class GetTreatmentDetailsForAppointmentDto
{
    public string Description { get; set; } = default!;
    public string Title { get; set; } = default!;
    public ImageDetailsDto Image { get; set; }=default!;
}
