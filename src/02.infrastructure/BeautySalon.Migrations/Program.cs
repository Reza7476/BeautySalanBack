using FluentMigrator.Runner;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class DataMigration
{
    private const string AppSettingPath = "appSettings.json";

    public static void Main(string[] arg)
    {
        var settings = GetSettings(arg, Directory.GetCurrentDirectory());
        var connectionString = settings.ConnectionString;

        EnsureDatabaseExist(connectionString);
        var runner = CreateRunner(connectionString);
        runner.MigrateDown(202508280913);
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

    private static MigrationSettings GetSettings(string[] args, string baseDir)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(baseDir)
            .AddJsonFile(AppSettingPath, optional: false, reloadOnChange: false)
            .AddEnvironmentVariables()
            .AddCommandLine(args)
            .Build();

        var settings = new MigrationSettings();
        config.GetSection("migrationConfig").Bind(settings);
        return settings;
    }
}

public class MigrationSettings
{
    public string ConnectionString { get; set; } = default!;
}