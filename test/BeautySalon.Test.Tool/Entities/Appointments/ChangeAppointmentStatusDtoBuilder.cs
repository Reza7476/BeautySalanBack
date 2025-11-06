using BeautySalon.Entities.Appointments;
using BeautySalon.Services.Appointments.Contracts.Dtos;

namespace BeautySalon.Test.Tool.Entities.Appointments;
public class ChangeAppointmentStatusDtoBuilder
{
    private readonly ChangeAppointmentStatusDto _dto;
    public ChangeAppointmentStatusDtoBuilder()
    {
        _dto= new ChangeAppointmentStatusDto()
        {
            Status=AppointmentStatus.Pending,
        };
    }

    public ChangeAppointmentStatusDtoBuilder WithId(string id)
    {
        _dto.Id=id;
        return this;
    }

    public ChangeAppointmentStatusDtoBuilder WithStatus( AppointmentStatus status)
    {
        _dto.Status=status;
        return  this;
    }


    public ChangeAppointmentStatusDto Build()
    {
        return _dto;
    }
}
