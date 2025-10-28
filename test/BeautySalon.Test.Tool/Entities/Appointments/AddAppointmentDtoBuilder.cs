using BeautySalon.Services.Appointments.Contracts.Dtos;

namespace BeautySalon.Test.Tool.Entities.Appointments;
public class AddAppointmentDtoBuilder
{
    private readonly AddAppointmentDto _dto;

    public AddAppointmentDtoBuilder()
    {
        _dto = new AddAppointmentDto()
        {
            AppointmentDate = DateTime.Now,
            Duration = 30,
        };
    }

    public AddAppointmentDtoBuilder WithClientId(string id)
    {
        _dto.ClientId = id;
        return this;
    }

    public AddAppointmentDtoBuilder WithTreatmentId(long id)
    {
        _dto.TreatmentId = id;
        return this;
    }

    public AddAppointmentDtoBuilder WithTechnicianId(string id)
    {
        _dto.TechnicianId = id;
        return this;
    }

    public AddAppointmentDtoBuilder WithDateTime(DateTime time)
    {
        _dto.AppointmentDate = time;
        return this;
    }

    public AddAppointmentDtoBuilder WithDuration(int number)
    {

        _dto.Duration = number;
        return this;
    }

    public AddAppointmentDto Build()
    {
        return _dto;
    }
}
