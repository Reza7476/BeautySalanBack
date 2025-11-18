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
