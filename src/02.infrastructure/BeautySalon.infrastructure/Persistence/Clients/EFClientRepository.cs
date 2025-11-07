using BeautySalon.Common.Dtos;
using BeautySalon.Common.Interfaces;
using BeautySalon.Entities.Appointments;
using BeautySalon.Entities.Clients;
using BeautySalon.Entities.Users;
using BeautySalon.infrastructure.Persistence.Extensions.Paginations;
using BeautySalon.Services.Clients.Contracts;
using BeautySalon.Services.Clients.Contracts.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.infrastructure.Persistence.Clients;
public class EFClientRepository : IClientRepository
{
    private readonly DbSet<Client> _clients;
    private readonly DbSet<Appointment> _appointments;
    private readonly DbSet<User> _users;

    public EFClientRepository(EFDataContext context)
    {
        _clients = context.Set<Client>();
        _appointments = context.Set<Appointment>();
        _users = context.Set<User>();
    }

    public async Task Add(Client client)
    {
        await _clients.AddAsync(client);
    }

    public Task<List<GetAllClientsForAddAppointment>>
        GetAllForAppointment(string? search = null)
    {
        var query = (from client in _clients
                     join user in _users on client.UserId equals user.Id
                     select new GetAllClientsForAddAppointment()
                     {
                         Id = client.Id,
                         LastName = user.LastName,
                         Name = user.Name,
                         MobileNumber = user.Mobile!,
                         Profile = user.Avatar != null ? new ImageDetailsDto()
                         {
                             Extension = user.Avatar.Extension!,
                             ImageName = user.Avatar.ImageName!,
                             UniqueName = user.Avatar.UniqueName!,
                             URL = user.Avatar.URL!
                         } : null

                     }).AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(_ => _.MobileNumber.Contains(search));
        }
        return query.ToListAsync();

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

    public async Task<bool> IsExistById(string id)
    {
        return await _clients.AnyAsync(_ => _.Id == id);
    }
}
