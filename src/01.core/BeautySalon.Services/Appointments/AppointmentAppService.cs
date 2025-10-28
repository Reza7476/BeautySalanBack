using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.Appointments;
using BeautySalon.Services.Appointments.Contracts;
using BeautySalon.Services.Appointments.Contracts.Dtos;
using BeautySalon.Services.Appointments.Exceptions;

namespace BeautySalon.Services.Appointments;
public class AppointmentAppService : IAppointmentService
{
    private readonly IAppointmentRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public AppointmentAppService(
        IAppointmentRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Add(AddAppointmentDto dto)
    {
        
        if (await _repository.CheckStatusForNewAppointment(dto.AppointmentDate))
        {
            throw new AppointmentIsBusyAtThisTimeException();
        }

        var appointment = new Appointment()
        {
            Id = Guid.NewGuid().ToString(),
            ClientId = dto.ClientId,
            TechnicianId = dto.TechnicianId,
            TreatmentId = dto.TreatmentId,
            AppointmentDate = dto.AppointmentDate,
            CreatedAt = DateTime.UtcNow,
            Duration = dto.Duration,
            EndTime = dto.AppointmentDate.AddMinutes(dto.Duration),
            Status = AppointmentStatus.Pending,
            Description = dto.Description,
        };
        await _repository.Add(appointment);
        await _unitOfWork.Complete();
        return appointment.Id;
    }
}
