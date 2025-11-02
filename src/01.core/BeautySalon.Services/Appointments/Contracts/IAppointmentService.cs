using BeautySalon.Common.Interfaces;
using BeautySalon.Services.Appointments.Contracts.Dtos;

namespace BeautySalon.Services.Appointments.Contracts;
public interface IAppointmentService : IService
{
    Task<string> Add(AddAppointmentDto dto);

    Task CancelByClient(string appointmentId, string clientId);

    Task<List<GetBookedAppointmentByDayDto>>
        GetBookAppointmentByDay(DateTime dateTime);
}
