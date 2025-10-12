namespace BeautySalon.Common.Interfaces;
public interface IPagination
{
    int? Offset { get; set; }
    int? Limit { get; set; }
}
