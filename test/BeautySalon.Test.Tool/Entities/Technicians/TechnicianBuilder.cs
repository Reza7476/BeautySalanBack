using BeautySalon.Entities.Technicians;

namespace BeautySalon.Test.Tool.Entities.Technicians;
public class TechnicianBuilder
{
    private readonly Technician _technician;

    public TechnicianBuilder()
    {
        _technician=new Technician()
        {
            Id=Guid.NewGuid().ToString(),
            CreatedDate=DateTime.Now,
        };
    }

    public TechnicianBuilder WithUser(string id)
    {
        _technician.UserId = id;
        return this;    
    }

    public Technician Build()
    {
        return _technician;
    }

}
