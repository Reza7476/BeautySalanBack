using BeautySalon.Entities.Commons;

namespace BeautySalon.Entities.WhyUsSections;
public class Why_Us_Section
{
    public Why_Us_Section()
    {
        Why_Us_Questions = new HashSet<Why_Us_Question>();
    }

    public long Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public MediaDocument Image { get; set; } = default!;
    public required DateTime CreateDate { get; set; }
    public HashSet<Why_Us_Question> Why_Us_Questions { get; set; }
}

public class Why_Us_Question
{
    public long Id { get; set; }
    public required string Question { get; set; }
    public required string Answer { get; set; }
    public long SectionId { get; set; }
    public required DateTime CreateDate { get; set; }
    public Why_Us_Section Section { get; set; } = default!;
}
