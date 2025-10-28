using BeautySalon.Application.Appointments.Contracts;
using BeautySalon.Application.Appointments.Contracts.Dtos;
using BeautySalon.Common.Interfaces;
using BeautySalon.Services.Appointments.Contracts;
using BeautySalon.Services.Appointments.Contracts.Dtos;
using BeautySalon.Services.Appointments.Exceptions;
using BeautySalon.Services.Clients.Contracts;
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

        if (!await _treatmentService.TreatmentIsExistById(dto.TreatmentId))
        {
            throw new TreatmentNotFoundException();
        }

        var appointmentId = await _appointmentService.Add(new AddAppointmentDto()
        {
            AppointmentDate = dto.AppointmentDate,
            Description=dto.Description,
            Duration=dto.Duration,
            ClientId=clientId,
            TechnicianId=technicianId,
            TreatmentId=dto.TreatmentId
        });
        
        return appointmentId;
    }
}
