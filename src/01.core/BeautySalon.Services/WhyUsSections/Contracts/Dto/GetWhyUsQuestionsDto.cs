namespace BeautySalon.Services.WhyUsSections.Contracts.Dto;
public class GetWhyUsQuestionsDto
{
    public long Id { get; set; }
    public required string Question { get; set; }
    public required string Answer { get; set; }

}
