using BeautySalon.Application.Users.Contracts;
using System;

namespace BeautySalon.RestApi.Configurations.RegisterAdmin;

public class AdminInitializer
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IHostEnvironment _env;

    public AdminInitializer(IServiceProvider serviceProvider,
        IHostEnvironment nev)
    {
        _serviceProvider = serviceProvider;
        _env = nev;
    }

    public void Initialize()
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var useHandler = scope.ServiceProvider.GetRequiredService<IUserHandle>();
            var (adminUser, adminPass) = AdminConfigReader.GetAdminCredential(_env.EnvironmentName, _env.ContentRootPath);

            try
            {
                useHandler.EnsureAdminIsExist(adminUser, adminPass).Wait();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
    }

}
