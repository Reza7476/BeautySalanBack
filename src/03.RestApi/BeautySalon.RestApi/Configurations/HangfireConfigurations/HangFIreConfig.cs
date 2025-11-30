using BeautySalon.RestApi.Configurations.ConnectionStrings;
using Hangfire;
using Hangfire.Dashboard.BasicAuthorization;
using Hangfire.SqlServer;

namespace BeautySalon.RestApi.Configurations.HangfireConfigurations;

public static class HangFIreConfig
{
    public static void HangFireConfigurationService(
           this IServiceCollection services,
           IWebHostEnvironment env)
    {

        var (appConfig, connectionString) = ConnectionStringConfig
                                           .LoadConfigAndConnectionStringForHangFire(
                                           env.EnvironmentName,
                                           env.ContentRootPath);


        if (connectionString != null)
        {
            services.AddHangfire(config =>
            {
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(connectionString, new SqlServerStorageOptions()
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                });
            });
            services.AddHangfireServer();
        }
    }

    public static IApplicationBuilder UseAppHangfire(
       this IApplicationBuilder app)
    {
        app.UseHangfireDashboard("/hangfire", new DashboardOptions
        {
            Authorization = new[]
            {
                 new BasicAuthAuthorizationFilter(
                     new BasicAuthAuthorizationFilterOptions
                     {
                         RequireSsl=false,
                         SslRedirect=false,
                         LoginCaseSensitive=true,
                         Users= new []
                         {
                             new BasicAuthAuthorizationUser
                             {
                                 Login="admin",
                                 PasswordClear="admin"
                             }
                         }
                     })
            },
            DisplayNameFunc = (_, job) =>
            {
                var argumentTypes =
                    string.Join(
                        ", ",
                        job.Args.Select(arg => arg?.GetType().Name));

                var className = job.Method.DeclaringType?.Name;
                return $"{className}.{job.Method.Name}: {argumentTypes}";
            }
        });
        return app;
    }
}
