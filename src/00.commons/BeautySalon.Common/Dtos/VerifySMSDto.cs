namespace BeautySalon.Common.Dtos;
public class VerifySMSDto
{
    public List<string> Results { get; set; } = new();
    public List<int> ResultsAsCode { get; set; } = new();
    public string? Status { get; set; }
}
