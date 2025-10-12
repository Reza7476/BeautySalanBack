namespace BeautySalon.Entities.Treatments;

public class Treatment
{
    public Treatment()
    {
        Images = new HashSet<TreatmentImage>();
    }

    public long Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required DateTime CreateDate { get; set; }
    public HashSet<TreatmentImage> Images { get; set; }
}

public class TreatmentImage
{
    public long Id { get; set; }
    public long TreatmentId { get; set; }
    public required string Extension { get; set; }
    public required string URL { get; set; }
    public required string ImageName { get; set; }
    public required string ImageUniqueName { get; set; }
    public required DateTime CreateDate { get; set; }
    public Treatment Treatment { get; set; } = default!;
}
