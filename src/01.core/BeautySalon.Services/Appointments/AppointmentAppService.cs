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
            Status = AppointmentStatus.Approved,
            DayWeek = dto.DayWeek,
        };
        await _repository.Add(appointment);
        await _unitOfWork.Complete();
        return appointment.Id;
    }

    public async Task CancelByClient(string appointmentId, string clientId)
    {
        var appointment = await _repository.FindByIdAndClientId(appointmentId, clientId);
        if (appointment == null)
        {
            throw new AppointmentNotFoundException();
        }

        appointment.CancelledAt = DateTime.UtcNow;
        appointment.CancelledBy = SystemRole.Client;
        appointment.Status = AppointmentStatus.Cancelled;
        await _unitOfWork.Complete();
    }

    public async Task ChangeStatus(ChangeAppointmentStatusDto dto)
    {
        var appointment = await _repository.FindById(dto.Id);
        if (appointment == null)
        {
            throw new AppointmentNotFoundException();
        }

        if (dto.Status == AppointmentStatus.Cancelled)
        {
            appointment.CancelledBy = SystemRole.Admin;
            appointment.CancelledAt = DateTime.UtcNow;
        }

        appointment.Status = dto.Status;

        await _unitOfWork.Complete();
    }

    public async Task<IPageResult<GetAllAdminAppointmentsDto>>
        GetAdminAllAppointments(IPagination? pagination = null,
        AdminAppointmentFilterDto? filter = null,
        string? search = null)
    {
        return await _repository.GetAdminAllAppointments(pagination, filter, search);
    }

    public async Task<IPageResult<GetAllAdminAppointmentsDto>> GetAllToday(
        IPagination? pagination = null,
        AdminAppointmentFilterDto? filter = null,
        string? search = null)
    {
        return await _repository.GetAllToday(pagination, filter, search);   
    }

    public async Task<List<GetAppointmentCountPerDayDto>> GetAppointmentPerDayForChart()
    {
        return await _repository.GetAppointmentPerDayForChart();
    }

    public async Task<List<GetBookedAppointmentByDayDto>>
        GetBookAppointmentByDay(DateTime dateTime)
    {
        return await _repository.GetBookAppointmentByDay(dateTime);
    }

    public async Task<GetAppointmentDetailsDto?> GetDetails(string id)
    {
        return await _repository.GetDetails(id);
    }
}
