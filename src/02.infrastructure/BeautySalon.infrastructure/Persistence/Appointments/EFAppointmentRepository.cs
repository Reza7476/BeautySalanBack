using BeautySalon.Common.Dtos;
using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.Appointments;
using BeautySalon.Entities.Clients;
using BeautySalon.Entities.Technicians;
using BeautySalon.Entities.Treatments;
using BeautySalon.Entities.Users;
using BeautySalon.Entities.WeeklySchedules;
using BeautySalon.infrastructure.Persistence.Extensions.Paginations;
using BeautySalon.Services.Appointments.Contracts;
using BeautySalon.Services.Appointments.Contracts.Dtos;
using BeautySalon.Services.Clients.Contracts.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.infrastructure.Persistence.Appointments;
public class EFAppointmentRepository : IAppointmentRepository
{
    private readonly DbSet<Appointment> _appointments;
    private readonly DbSet<Client> _clients;
    private readonly DbSet<Technician> _technicians;
    private readonly DbSet<Treatment> _treatments;
    private readonly DbSet<User> _users;

    public EFAppointmentRepository(EFDataContext context)
    {
        _appointments = context.Set<Appointment>();
        _clients = context.Set<Client>();
        _technicians = context.Set<Technician>();
        _treatments = context.Set<Treatment>();
        _users = context.Set<User>();
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

    public async Task<Appointment?> FindById(string id)
    {
        return await _appointments.FindAsync(id);
    }

    public async Task<Appointment?> FindByIdAndClientId(string appointmentId, string clientId)
    {
        return await _appointments
            .Where(_ => _.Id == appointmentId && _.ClientId == clientId)
            .FirstOrDefaultAsync();
    }

    public async Task<IPageResult<GetAllAdminAppointmentsDto>>
        GetAdminAllAppointments(
        IPagination? pagination = null,
        AdminAppointmentFilterDto? filter = null,
        string? search = null)
    {
        var query = (from appointment in _appointments
                     join client in _clients on appointment.ClientId equals client.Id
                     join user in _users on client.UserId equals user.Id
                     join treatment in _treatments on appointment.TreatmentId equals treatment.Id
                     select new GetAllAdminAppointmentsDto()
                     {
                         ClientName = user.Name,
                         ClientLastName = user.LastName,
                         ClientMobile = user.Mobile!,
                         AppointmentDate = DateOnly.FromDateTime(appointment.AppointmentDate),
                         DayWeek = appointment.DayWeek,
                         Duration = appointment.Duration,
                         EndTime = TimeOnly.FromDateTime(appointment.EndTime),
                         StartTime = TimeOnly.FromDateTime(appointment.AppointmentDate),
                         Id = appointment.Id,
                         Status = appointment.Status,
                         TreatmentTitle = treatment.Title,
                     }).AsQueryable();

        if (filter != null)
        {
            if (filter.Status != 0)
            {
                query = query.Where(_ => _.Status == filter.Status);
            }

            if (filter.DayWeek != 0)
            {
                query = query.Where(_ => _.DayWeek == filter.DayWeek);
            }

            if (filter.Date > new DateOnly(1, 1, 1))
            {
                query = query.Where(_ => _.AppointmentDate == filter.Date);
            }

            if (filter.TreatmentTitle != null)
            {
                query = query.Where(_ => _.TreatmentTitle == filter.TreatmentTitle);
            }
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            var lowered = search.ToLower();
            query = query.Where(_ => _.ClientMobile.Contains(lowered));
        }
        query = query.OrderByDescending(_ => _.AppointmentDate);
        return await query.Paginate(pagination ?? new Pagination());
    }

    public async Task<GetDashboardAdminSummaryDto?> GetAdminDashboardSummary()
    {
        var today = DateTime.UtcNow.Date;
        var query = new GetDashboardAdminSummaryDto()
        {
            TodayAppointments = await _appointments.Where(_ => _.AppointmentDate.Date == today).CountAsync(),
            TotalClients = await _clients.CountAsync(),
            TotalTreatments = await _treatments.CountAsync()
        };
        return query;

    }

    public async Task<IPageResult<GetAllAdminAppointmentsDto>>
        GetAllToday(IPagination? pagination = null,
        AdminAppointmentFilterDto? filter = null,
        string? search = null)
    {
        var query = (from appointment in _appointments
                     join client in _clients on appointment.ClientId equals client.Id
                     join user in _users on client.UserId equals user.Id
                     join treatment in _treatments on appointment.TreatmentId equals treatment.Id
                     where appointment.AppointmentDate.Date == DateTime.UtcNow.Date
                     select new GetAllAdminAppointmentsDto()
                     {
                         ClientName = user.Name,
                         ClientLastName = user.LastName,
                         ClientMobile = user.Mobile!,
                         AppointmentDate = DateOnly.FromDateTime(appointment.AppointmentDate),
                         DayWeek = appointment.DayWeek,
                         Duration = appointment.Duration,
                         EndTime = TimeOnly.FromDateTime(appointment.EndTime),
                         StartTime = TimeOnly.FromDateTime(appointment.AppointmentDate),
                         Id = appointment.Id,
                         Status = appointment.Status,
                         TreatmentTitle = treatment.Title,
                     }).AsQueryable();


        if (filter != null)
        {
            if (filter.Status != 0)
            {
                query = query.Where(_ => _.Status == filter.Status);
            }

            if (filter.TreatmentTitle != null)
            {
                query = query.Where(_ => _.TreatmentTitle == filter.TreatmentTitle);
            }
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            var lowered = search.ToLower();
            query = query.Where(_ => _.ClientMobile.Contains(lowered));
        }
        query = query.OrderByDescending(_ => _.AppointmentDate);
        return await query.Paginate(pagination ?? new Pagination());
    }

    public async Task<List<GetAppointmentCountPerDayDto>> GetAppointmentPerDayForChart()
    {
        var today = DateTime.UtcNow;
        var startDate = today.AddDays(-6);
        List<GetAppointmentCountPerDayDto> appointment = new List<GetAppointmentCountPerDayDto>();
        var query = await _appointments
            .Where(_ => _.AppointmentDate.Date >= startDate &&
                   _.AppointmentDate.Date <= today)
            .GroupBy(_ => _.DayWeek)
            .Select(g => new GetAppointmentCountPerDayDto
            {
                Count = g.Count(),
                DayWeek = g.Key
            }).ToListAsync();
        var result = Enumerable.Range(1, 7)
       .Select(i => query.FirstOrDefault(x => (int)x.DayWeek == i)
                    ?? new GetAppointmentCountPerDayDto
                    {
                        DayWeek = (DayWeek)i,
                        Count = 0
                    })
       .ToList();

        return result;
    }

    public async Task<List<GetAppointmentRequiringSMSDto>> GetAppointmentRequiringSMS()
    {

        var tomorrow = DateTime.UtcNow.Date.AddDays(1);
        var dayAfterTomorrow = tomorrow.AddDays(1);
        var query = await (from appointment in _appointments
                           join client in _clients on appointment.ClientId equals client.Id
                           join user in _users on client.UserId equals user.Id
                           join treatment in _treatments on appointment.TreatmentId equals treatment.Id
                           where appointment.Status == AppointmentStatus.Approved &&
                                 appointment.AppointmentDate.Date >= tomorrow &&
                                 appointment.AppointmentDate.Date < dayAfterTomorrow &&
                                 appointment.RemindSMSSent != true
                           select new GetAppointmentRequiringSMSDto
                           {
                               AppointmentId = appointment.Id,
                               TreatmentTitle = treatment.Title,
                               ClientName = user.Name,
                               ClientLastName = user.LastName,
                               ClientNumber = user.Mobile,
                               AppointmentData = appointment.AppointmentDate
                           }).ToListAsync();
        return query;
    }

    public async Task<List<GetBookedAppointmentByDayDto>>
        GetBookAppointmentByDay(DateTime dateTime)
    {
        return await _appointments.Where(
         _ => _.AppointmentDate.Date == dateTime.Date &&
        (_.Status == AppointmentStatus.Pending ||
         _.Status == AppointmentStatus.Confirmed ||
         _.Status == AppointmentStatus.Approved))
         .Select(a => new GetBookedAppointmentByDayDto()
         {
             Duration = a.Duration,
             StartDate = TimeOnly.FromDateTime(a.AppointmentDate),
             EndDate = TimeOnly.FromDateTime(a.EndTime),
         }).ToListAsync();
    }

    public async Task<List<Appointment>> GetByIds(List<string> listIds)
    {
        return await _appointments
       .Where(x => listIds.Contains(x.Id))
       .ToListAsync();
    }

    public async Task<IPageResult<GetAllClientAppointmentsDto>> GetClientAppointments(
        string id,
        IPagination? pagination = null,
        ClientAppointmentFilterDto? filterDto = null)
    {
        var query = _appointments.Where(_ => _.ClientId == id)
            .Include(_ => _.Treatment)
            .Select(_ => new GetAllClientAppointmentsDto()
            {
                Id = _.Id,
                TreatmentTitle = _.Treatment.Title,
                Duration = _.Treatment.Duration,
                StartTime = TimeOnly.FromDateTime(_.AppointmentDate),
                EndTime = TimeOnly.FromDateTime(_.EndTime),
                DayWeek = _.DayWeek,
                Status = _.Status,
                CancelledBy = _.CancelledBy,
                AppointmentDate = DateOnly.FromDateTime(_.AppointmentDate),
                CancelledDate = DateOnly.FromDateTime(_.CancelledAt),
                CreatedAt = DateOnly.FromDateTime(_.CreatedAt),
                Reviewed = _.Review != null ? true : false
            }).AsQueryable();

        if (filterDto != null)
        {
            if (filterDto.Date > new DateOnly(1, 1, 1))
            {
                query = query.Where(_ => _.AppointmentDate == filterDto.Date);
            }

            if (filterDto.DayWeek != 0)
            {
                var numberDay = (int)filterDto.DayWeek;
                query = query.Where(_ => (int)_.DayWeek == numberDay);
            }

            if (filterDto.Status != 0)
            {
                query = query.Where(_ => _.Status == filterDto.Status);
            }
        }
        query = query.OrderByDescending(_ => _.AppointmentDate);
        return await query.Paginate(pagination ?? new Pagination());
    }

    public async Task<string?> GetClientIdByUserId(string userId)
    {
        return await _clients
            .Where(_ => _.UserId == userId)
            .Select(c => c.Id)
            .FirstOrDefaultAsync();
    }

    public async Task<DashboardClientSummaryDto?> GetDashboardClientSummary(string userId)
    {
        var query = await (
                    from appointment in _appointments
                    join client in _clients on appointment.ClientId equals client.Id
                    join user in _users
                    on client.UserId equals user.Id
                    join treatment in _treatments
                    on appointment.TreatmentId equals treatment.Id
                    where user.Id == userId
                    select new
                    {
                        treatment.Title,
                        appointment.DayWeek,
                        appointment.AppointmentDate,
                        appointment.Status
                    }).ToListAsync();

        if (!query.Any()) return null;
        var dto = new DashboardClientSummaryDto()
        {
            FutureAppointments = query
            .Where(_ => DateOnly.FromDateTime(_.AppointmentDate) >= DateOnly.FromDateTime(DateTime.UtcNow))
            .Select(f => new DashboardClientAppointmentDto()
            {
                Date = DateOnly.FromDateTime(f.AppointmentDate),
                Day = f.DayWeek,
                Start = TimeOnly.FromDateTime(f.AppointmentDate),
                Status = f.Status,
                TreatmentTitle = f.Title
            }).ToList(),
            FormerAppointments = query
            .Where(_ => DateOnly.FromDateTime(_.AppointmentDate) < DateOnly.FromDateTime(DateTime.UtcNow))
            .Select(f => new DashboardClientAppointmentDto()
            {
                Date = DateOnly.FromDateTime(f.AppointmentDate),
                Day = f.DayWeek,
                Start = TimeOnly.FromDateTime(f.AppointmentDate),
                Status = f.Status,
                TreatmentTitle = f.Title
            }).ToList(),
        };
        return dto;
    }

    public async Task<GetAppointmentDetailsDto?> GetDetails(string id)
    {
        return await (from appointment in _appointments
                      join client in _clients on appointment.ClientId equals client.Id
                      join treatment in _treatments on appointment.TreatmentId equals treatment.Id
                      join user in _users on client.UserId equals user.Id
                      where appointment.Id == id
                      select new GetAppointmentDetailsDto()
                      {
                          ClientLastName = user.LastName,
                          ClientName = user.Name,
                          ClientMobile = user.Mobile!,
                          Date = DateOnly.FromDateTime(appointment.AppointmentDate),
                          Day = appointment.DayWeek,
                          Duration = appointment.Duration,
                          EndTime = TimeOnly.FromDateTime(appointment.EndTime),
                          StartTime = TimeOnly.FromDateTime(appointment.AppointmentDate),
                          Status = appointment.Status,
                          TreatmentTitle = treatment.Title,
                          Price = treatment.Price
                      }).FirstOrDefaultAsync();
    }

    public async Task<List<GetNewAppointmentsDashboardDto>> GetNewAppointmentDashboard()
    {
        var query = await (from appointment in _appointments
                           join client in _clients on appointment.ClientId equals client.Id
                           join user in _users on client.UserId equals user.Id
                           join treatment in _treatments on appointment.TreatmentId equals treatment.Id
                           where appointment.Status == AppointmentStatus.Pending
                           select new GetNewAppointmentsDashboardDto()
                           {
                               ClientLastName = user.LastName,
                               ClientName = user.Name,
                               Date = DateOnly.FromDateTime(appointment.AppointmentDate),
                               DayWeek = appointment.DayWeek,
                               Mobile = user.Mobile,
                               Status = appointment.Status,
                               TreatmentTitle = treatment.Title
                           }).ToListAsync();
        return query;
    }

    public async Task<List<Appointment>> GetOverdueUnfinalizedAppointments()
    {
        return await _appointments
        .Where(_ =>
        _.AppointmentDate < DateTime.UtcNow &&
        _.Status != AppointmentStatus.Cancelled &&
        _.Status != AppointmentStatus.Completed &&
        _.Status != AppointmentStatus.Pending).ToListAsync();
    }

    public async Task<IPageResult<GetAllAdminAppointmentsDto>> GetPendingAppointment(
        IPagination? pagination = null,
        AdminAppointmentFilterDto? filter = null,
        string? search = null)
    {
        var query = (from appointment in _appointments
                     join client in _clients on appointment.ClientId equals client.Id
                     join user in _users on client.UserId equals user.Id
                     join treatment in _treatments on appointment.TreatmentId equals treatment.Id
                     where appointment.Status == AppointmentStatus.Pending
                     select new GetAllAdminAppointmentsDto()
                     {
                         ClientName = user.Name,
                         ClientLastName = user.LastName,
                         ClientMobile = user.Mobile!,
                         AppointmentDate = DateOnly.FromDateTime(appointment.AppointmentDate),
                         DayWeek = appointment.DayWeek,
                         Duration = appointment.Duration,
                         EndTime = TimeOnly.FromDateTime(appointment.EndTime),
                         StartTime = TimeOnly.FromDateTime(appointment.AppointmentDate),
                         Id = appointment.Id,
                         Status = appointment.Status,
                         TreatmentTitle = treatment.Title,
                     }).AsQueryable();


        if (filter != null)
        {

            if (filter.TreatmentTitle != null)
            {
                query = query.Where(_ => _.TreatmentTitle == filter.TreatmentTitle);
            }

            if (filter.DayWeek != 0)
            {
                query = query.Where(_ => _.DayWeek == filter.DayWeek);
            }

            if (filter.Date > new DateOnly(1, 1, 1))
            {
                query = query.Where(_ => _.AppointmentDate == filter.Date);
            }
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            var lowered = search.ToLower();
            query = query.Where(_ => _.ClientMobile.Contains(lowered));
        }
        query = query.OrderByDescending(_ => _.AppointmentDate);
        return await query.Paginate(pagination ?? new Pagination());
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
