
using BeautySalon.infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;

namespace BeautySalon.Test.Tool.Infrastructure.Integration;

public class BusinessIntegrationTest : IDisposable
{
    protected EFDataContext DbContext { get; set; } 

    private IDbContextTransaction _transaction;

    public BusinessIntegrationTest()
    {
        DbContext = CreateDataContext();
        BeginTransaction();
    }


    private void BeginTransaction()
    {
        _transaction = DbContext.Database.BeginTransaction();
    }

    protected void Save<T>(params T[] entities) where T : class
    {
        foreach (var entity in entities)
        {
            DbContext.Set<T>().Add(entity);
        }
        DbContext.SaveChanges();
    }

    public void Dispose()
    {
        try
        {
            _transaction.Rollback();
            _transaction.Dispose();
            DbContext.Dispose();
        }
        finally
        {
            _transaction.Dispose();
            DbContext.Dispose();
        }
    }

    private static EFDataContext CreateDataContext()
    {
        var settings = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("testAppSettings.json", optional: true, reloadOnChange: false)
            .AddEnvironmentVariables()
            .AddCommandLine(Environment.GetCommandLineArgs())
            .Build();

        var testSettings = new InfrastructureConfig();
        settings.Bind("PersistenceConfig", testSettings);

        var optionsBuilder = new DbContextOptionsBuilder<EFDataContext>();
        optionsBuilder.UseSqlServer(testSettings.ConnectionString);

        return new EFDataContext(optionsBuilder.Options);
    }
}

public class InfrastructureConfig
{
    public string ConnectionString { get; set; } = default!;
}
