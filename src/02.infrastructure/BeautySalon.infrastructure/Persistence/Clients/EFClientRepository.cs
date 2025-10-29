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
        ClientAppointmentFilterDto? filter = null)
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
                DayWeek = (DayWeek)(_.AppointmentDate.DayOfWeek + 1),
                Status = _.Status,
                CancelledBy = _.CancelledBy,
              //  CancelledDate = _.CancelledAt != null ? DateOnly.FromDateTime(_.CancelledAt) : null,
                CreatedAt = DateOnly.FromDateTime(_.CreatedAt),
            }).AsQueryable();



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
