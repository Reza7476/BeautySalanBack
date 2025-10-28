using BeautySalon.Application.Appointments.Contracts.Dtos;
using BeautySalon.Common.Interfaces;

namespace BeautySalon.Application.Appointments.Contracts;
public interface IAppointmentHandler : IScope
{
    Task<string> AddAppointment(AddAppointmentHandlerDto dto, string userId);
}
