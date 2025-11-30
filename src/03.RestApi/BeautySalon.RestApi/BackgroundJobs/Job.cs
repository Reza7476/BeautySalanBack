using BeautySalon.RestApi.Configurations.ConnectionStrings;
using BeautySalon.Services.Appointments.Jobs;
using Dapper;
using Hangfire;
using Microsoft.Data.SqlClient;

namespace BeautySalon.RestApi.BackgroundJobs;

public static class Job
{
    public static void RegisterHangfireJobs(
        this IApplicationBuilder app,
        IWebHostEnvironment env)
    {
        var (appConfig, connectionString) = ConnectionStringConfig
                                           .LoadConfigAndConnectionStringForHangFire(
                                           env.EnvironmentName,
                                           env.ContentRootPath);

        var recurringManager = app.ApplicationServices
            .GetRequiredService<IRecurringJobManager>();


        recurringManager.AddOrUpdate<AppointmentJobs>("FixAppointmentStatus",

            myJob => myJob.ChangeOverdueStatusByHangFire(),
            "0 23 * * *");


        recurringManager.AddOrUpdate(
            "HangfireCleanupJob",
            () => CleanupOldJobs(connectionString),
            Cron.Daily());
    }

    public static void CleanupOldJobs(string connectionString)
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        var thresholdDate = DateTime.UtcNow.AddDays(-7);

        // پاکسازی Jobهای موفق قدیمی
        connection.Execute(
            "DELETE FROM Hangfire.Job WHERE StateName = 'Succeeded' AND CreatedAt < @ThresholdDate",
            new { ThresholdDate = thresholdDate });

        // پاکسازی Stateهای موفق قدیمی
        connection.Execute(
            "DELETE FROM Hangfire.State WHERE Name = 'Succeeded' AND CreatedAt < @ThresholdDate",
            new { ThresholdDate = thresholdDate });

        // پاکسازی Counterهای منقضی شده
        connection.Execute("DELETE FROM Hangfire.Counter WHERE ExpireAt < GETUTCDATE()");

        // پاکسازی AggregatedCounterهای منقضی شده
        connection.Execute("DELETE FROM Hangfire.AggregatedCounter WHERE ExpireAt < GETUTCDATE()");
    }

}
