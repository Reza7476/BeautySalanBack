namespace BeautySalon.Services.AppointmentReviews.Contracts.Dtos;
public class GetAllReviewsDto
{
    public string TreatmentTitle { get; set; } = default!;
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public bool IsPublished { get; set; }
    public byte Rate { get; set; }
    public string Id { get; set; }
}
