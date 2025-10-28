using BeautySalon.Entities.Clients;
using BeautySalon.Services.Clients.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.infrastructure.Persistence.Clients;
public class EFClientRepository : IClientRepository
{
    private readonly DbSet<Client> _clients;

    public EFClientRepository(EFDataContext context)
    {
        _clients = context.Set<Client>();
    }

    public async Task Add(Client client)
    {
     await _clients.AddAsync(client);
    }

    public async Task<string?> GetClientIdByUserId(string userId)
    {
        return await _clients
             .Where(_ => _.UserId == userId)
             .Select(c => c.Id)
             .FirstOrDefaultAsync();
    }
}
