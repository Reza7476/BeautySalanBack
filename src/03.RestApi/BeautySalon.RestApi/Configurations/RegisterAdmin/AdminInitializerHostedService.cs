namespace BeautySalon.RestApi.Configurations.RegisterAdmin;

using Microsoft.Extensions.Hosting;

public class AdminInitializerHostedService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public AdminInitializerHostedService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var adminInitializer = scope.ServiceProvider.GetRequiredService<AdminInitializer>();

        try
        {
            await Task.Delay(2000, cancellationToken); // 👈 کمی تأخیر برای اطمینان از آماده بودن context
            adminInitializer.Initialize();
            Console.WriteLine("✅ Admin initialization done.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ Failed to initialize admin: {ex.Message}");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
