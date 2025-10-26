namespace BeautySalon.Services.Treatments.Contracts.Dto;
public class GetAllTreatmentsForAppointmentDto
{
    public long  Id { get; set; }

    public required string Title { get; set; } = default!;
}
