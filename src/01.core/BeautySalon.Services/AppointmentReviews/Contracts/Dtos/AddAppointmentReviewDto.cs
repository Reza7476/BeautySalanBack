namespace BeautySalon.Services.AppointmentReviews.Contracts.Dtos;
public class AddAppointmentReviewDto
{
    public string AppointmentId { get; set; } = default!;
    public string? Description { get; set;} 
    public byte Rate { get; set; }
}
