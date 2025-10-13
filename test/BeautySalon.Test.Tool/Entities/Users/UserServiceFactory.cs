using BeautySalon.infrastructure;
using BeautySalon.infrastructure.Persistence.Users;
using BeautySalon.Services.Users;

namespace BeautySalon.Test.Tool.Entities.Users;
public  static class UserServiceFactory
{
    public static UserAppService Generate(EFDataContext context)
    {
        var repository= new EFUserRepository(context);
        var unitOfWork = new EFUnitOfWork(context);

        return new UserAppService(repository, unitOfWork);  
    }
}
