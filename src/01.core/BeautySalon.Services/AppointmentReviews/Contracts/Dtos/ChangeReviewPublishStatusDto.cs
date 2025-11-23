namespace BeautySalon.Services.AppointmentReviews.Contracts.Dtos;
public class ChangeReviewPublishStatusDto
{
    public string Id { get; set; } = default!;
    public bool PublishStatus { get; set; }
}
