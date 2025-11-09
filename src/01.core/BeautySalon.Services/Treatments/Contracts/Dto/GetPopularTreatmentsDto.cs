using BeautySalon.Common.Dtos;

namespace BeautySalon.Services.Treatments.Contracts.Dto;
public class GetPopularTreatmentsDto
{
    public ImageDetailsDto? Image { get; set; }
    public string? Title { get; set; }
    public int AppointmentCount { get; set; }
    public long Id { get; set; }

}
