using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.Appointments;
using BeautySalon.Entities.Clients;
using BeautySalon.Entities.WeeklySchedules;
using BeautySalon.infrastructure.Persistence.Extensions.Paginations;
using BeautySalon.Services.Clients.Contracts;
using BeautySalon.Services.Clients.Contracts.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.infrastructure.Persistence.Clients;
public class EFClientRepository : IClientRepository
{
    private readonly DbSet<Client> _clients;
    private readonly DbSet<Appointment> _appointments;


    public EFClientRepository(EFDataContext context)
    {
        _clients = context.Set<Client>();
        _appointments = context.Set<Appointment>();
    }

    public async Task Add(Client client)
    {
        await _clients.AddAsync(client);
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
            }).AsQueryable();

        if (filterDto != null)
        {
            if (filterDto.Date > new DateOnly(1, 1, 1))
            {
                query = query.Where(_ => _.AppointmentDate == filterDto.Date);
            }

            if (filterDto.Day != 0)
            {
                var numberDay = (int)filterDto.Day;
                query = query.Where(_ => (int)_.DayWeek == numberDay);
            }

            if (filterDto.Status != 0)
            {
                query = query.Where(_ => _.Status == filterDto.Status);
            }
        }
        query.OrderByDescending(_ => _.AppointmentDate);
        return await query.Paginate(pagination ?? new Pagination());
    }

    public async Task<string?> GetClientIdByUserId(string userId)
    {
        return await _clients
             .Where(_ => _.UserId == userId)
             .Select(c => c.Id)
             .FirstOrDefaultAsync();
    }
}
