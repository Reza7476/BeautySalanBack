namespace BeautySalon.Common.Interfaces;

public interface ISort
{
    string? Expression { get; }

    Dictionary<string, string> GetSortParameters();
}
