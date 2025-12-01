using BeautySalon.Application.Appointments.Contracts;
using BeautySalon.Application.Appointments.Contracts.Dtos;
using BeautySalon.Entities.Appointments;
using BeautySalon.Services.Appointments.Contracts;
using BeautySalon.Services.Appointments.Contracts.Dtos;
using BeautySalon.Services.Appointments.Exceptions;
using BeautySalon.Services.Clients.Contracts;
using BeautySalon.Services.Clients.Exceptions;
using BeautySalon.Services.Technicians.Contracts;
using BeautySalon.Services.Technicians.Exceptions;
using BeautySalon.Services.Treatments.Contracts;
using BeautySalon.Services.Treatments.Exceptions;

namespace BeautySalon.Application.Appointments;
public class AppointmentCommandHandler : IAppointmentHandler
{
    private readonly IAppointmentService _appointmentService;
    private readonly ITreatmentService _treatmentService;
    private readonly IClientService _clientService;
    private readonly ITechnicianService _technicianService;

    public AppointmentCommandHandler(
        IAppointmentService appointmentService,
        ITreatmentService treatmentService,
        IClientService clientService,
        ITechnicianService technicianService)
    {
        _appointmentService = appointmentService;
        _treatmentService = treatmentService;
        _clientService = clientService;
        _technicianService = technicianService;
    }

    public async Task<string> AddAdminAppointment(AddAdminAppointmentHandlerDto dto)
    {
        if (!await _clientService.IsExistById(dto.ClientId))
        {
            throw new ClientNotFoundException();
        }
        if (!await _treatmentService.IsExistById(dto.TreatmentId))
        {
            throw new TreatmentNotFoundException();
        }
        var technicianId = await _technicianService.GetTechnicianId();
        if (technicianId == null)
        {
            throw new TechnicianNotDefinedException();
        }
        var appointmentId = await _appointmentService.Add(new AddAppointmentDto()
        {
            AppointmentDate = dto.AppointmentDate,
            Duration = dto.Duration,
            ClientId = dto.ClientId,
            TechnicianId = technicianId,
            TreatmentId = dto.TreatmentId,
            DayWeek = dto.DayWeek,
            Status = AppointmentStatus.Approved
        });

        return appointmentId;
    }

    public async Task<string> AddAppointment(AddAppointmentHandlerDto dto, string userId)
    {
        var clientId = await _clientService.GetClientIdByUserId(userId);
        if (clientId == null)
        {
            throw new UserNotRegisterAsClientException();
        }
        var technicianId = await _technicianService.GetTechnicianId();
        if (technicianId == null)
        {
            throw new TechnicianNotDefinedException();
        }

        if (!await _treatmentService.IsExistById(dto.TreatmentId))
        {
            throw new TreatmentNotFoundException();
        }

        var appointmentId = await _appointmentService.Add(new AddAppointmentDto()
        {
            AppointmentDate = dto.AppointmentDate,
            Duration = dto.Duration,
            ClientId = clientId,
            TechnicianId = technicianId,
            TreatmentId = dto.TreatmentId,
            DayWeek = dto.DayWeek,
            Status = AppointmentStatus.Pending
        });

        return appointmentId;
    }

    public async Task CancelAppointmentByClient(string appointmentId, string? userId)
    {

        if (userId == null)
        {
            throw new YouAreNotAllowedToAccessException();
        }
        var clientId = await _clientService.GetClientIdByUserId(userId!);
        if (clientId == null)
        {
            throw new UserNotRegisterAsClientException();
        }
        await _appointmentService.CancelByClient(appointmentId, clientId);

    }
}
