using BeautySalon.Entities.Appointments;
using BeautySalon.Entities.Clients;
using BeautySalon.Entities.Technicians;
using BeautySalon.Entities.Treatments;
using BeautySalon.Services.Appointments.Contracts;
using BeautySalon.Services.Appointments.Contracts.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.infrastructure.Persistence.Appointments;
public class EFAppointmentRepository : IAppointmentRepository
{
    private readonly DbSet<Appointment> _appointments;
    private readonly DbSet<Client> _clients;
    private readonly DbSet<Technician> _technicians;
    private readonly DbSet<Treatment> _treatments;

    public EFAppointmentRepository(EFDataContext context)
    {
        _appointments = context.Set<Appointment>();
        _clients = context.Set<Client>();
        _technicians = context.Set<Technician>();
        _treatments = context.Set<Treatment>();
    }

    public async Task Add(Appointment appointment)
    {
        await _appointments.AddAsync(appointment);
    }

    public async Task<bool> CheckStatusForNewAppointment(DateTime appointmentDate)
    {
        var b = await _appointments
            .AnyAsync(a =>
            (a.Status == AppointmentStatus.Pending ||
            a.Status == AppointmentStatus.Confirmed) &&
            appointmentDate >= a.AppointmentDate &&
            appointmentDate < a.EndTime);
        return b;
    }

    public async Task<Appointment?> FindByIdAndClientId(string appointmentId, string clientId)
    {
        return await _appointments
            .Where(_ => _.Id == appointmentId && _.ClientId == clientId)
            .FirstOrDefaultAsync();
    }

    public async Task<List<GetBookedAppointmentByDayDto>>
        GetBookAppointmentByDay(DateTime dateTime)
    {
        return await _appointments.Where(
         _ => _.AppointmentDate.Date == dateTime.Date &&
        (_.Status == AppointmentStatus.Pending ||
         _.Status == AppointmentStatus.Confirmed))
         .Select(a => new GetBookedAppointmentByDayDto()
         {
             Duration = a.Duration,
             StartDate = TimeOnly.FromDateTime(a.AppointmentDate),
             EndDate = TimeOnly.FromDateTime(a.EndTime),
         }).ToListAsync();
    }

    public async Task<string?> GetClientIdByUserId(string userId)
    {
        return await _clients
            .Where(_ => _.UserId == userId)
            .Select(c => c.Id)
            .FirstOrDefaultAsync();
    }

    public async Task<string?> GetTechnicianId()
    {
        var technician = await _technicians
           .AsNoTracking()
           .FirstOrDefaultAsync();
        return technician?.Id;
    }

    public async Task<bool> TreatmentIsExistById(long treatmentId)
    {
        return await _treatments.AnyAsync(_ => _.Id == treatmentId);
    }
}
