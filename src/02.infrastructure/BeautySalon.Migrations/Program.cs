using FluentMigrator.Runner;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class DataMigration
{
    private const string AppSettingPath = "appsettings.json";

    public static void Main(string[] args)
    {
        var baseDir = Directory.GetCurrentDirectory();
        var connectionString = GetConnectionString(baseDir);
        var settings = new MigrationSettings
        {
            ConnectionString = connectionString
        };
        EnsureDatabaseExist(connectionString);
        var runner = CreateRunner(connectionString);
       /// runner.MigrateDown(0); 
       runner.MigrateUp();
    }

    private static void EnsureDatabaseExist(string connectionString)
    {
        var dbName = new SqlConnectionStringBuilder(connectionString).InitialCatalog;

        var masterConnectionString = new SqlConnectionStringBuilder(connectionString)
        {
            InitialCatalog = "master"
        }.ConnectionString;

        var query = $"IF DB_ID(N'{dbName}') IS NULL CREATE DATABASE [{dbName}]";

        using var connection = new SqlConnection(masterConnectionString);
        using var command = new SqlCommand(query, connection);
        connection.Open();
        command.ExecuteNonQuery();
    }

    private static IMigrationRunner CreateRunner(string connectionString)
    {
        var services = new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddSqlServer()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(typeof(DataMigration).Assembly).For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            .BuildServiceProvider();

        return services.GetRequiredService<IMigrationRunner>();
    }
   
    public static string GetConnectionString(string baseDir)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(baseDir)
            .AddJsonFile(AppSettingPath, optional: false, reloadOnChange: false)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = config["migrationConfig:connectionString"]
                               ?? throw new InvalidOperationException("Connection string not found in configuration.");

        var dbUser = Environment.GetEnvironmentVariable("DB_USER");
        var dbPass = Environment.GetEnvironmentVariable("DB_PASS");

        if (!string.IsNullOrEmpty(dbUser))
            connectionString = connectionString.Replace("${DB_USER}", dbUser);

        if (!string.IsNullOrEmpty(dbPass))
            connectionString = connectionString.Replace("${DB_PASS}", dbPass);

        return connectionString;
    }
}

public class MigrationSettings
{
    public string ConnectionString { get; set; } = default!;
}