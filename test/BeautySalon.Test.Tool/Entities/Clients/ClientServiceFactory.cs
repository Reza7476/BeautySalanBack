using BeautySalon.infrastructure;
using BeautySalon.infrastructure.Persistence.Clients;
using BeautySalon.Services.Clients;

namespace BeautySalon.Test.Tool.Entities.Clients;
public static class ClientServiceFactory
{
    public static ClientAppService Generate(EFDataContext context)
    {

        var repository = new EFClientRepository(context);
        var unitOfWork = new EFUnitOfWork(context);

        return new ClientAppService(repository, unitOfWork);
    }
}
