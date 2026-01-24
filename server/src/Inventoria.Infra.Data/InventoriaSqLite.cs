using System.Collections.Concurrent;
using System.Data;
using System.Reflection;
using Dapper;
using FluentMigrator.Runner;
using Inventoria.Core.Domain.Abstractions;
using Inventoria.Core.Domain.Database;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Inventoria.Infra.Data;

public class InventoriaSqLite : IInventoriaDatabase
{
    private readonly ConcurrentStack<IDbConnection> connections = new();
    private readonly string connectionString = "Data Source=./inventoria.db;";

    public InventoriaSqLite()
    {
    }

    public InventoriaSqLite(string? customConnectionString)
    {
        if (!string.IsNullOrWhiteSpace(customConnectionString))
            connectionString = customConnectionString;
    }

    public InventoriaSqLite(IConfiguration configuration)
    {
        connectionString = configuration.GetSection("ConnectionStrings:SqLite").Value ?? connectionString;
    }

    public IDbConnection CreateDbConnection()
    {
        return new SqliteConnection(connectionString);
    }

    public IDbConnection GetDbConnection()
    {
        if (connections.TryPop(out var connection))
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            return connection;
        }

        var conn = CreateDbConnection();
        conn.Open();
        return conn;
    }

    public void ReturnDbConnection(IDbConnection connection)
    {
        if (connection.State == ConnectionState.Open)
            connections.Push(connection);
        else
            connection.Dispose();
    }

    public void ApplyMigrations()
    {
        var serviceProvider = new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddSQLite()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            .BuildServiceProvider(false);

        using var scope = serviceProvider.CreateScope();
        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();
    }

    public IEnumerable<MigrationHistoric> CheckHistory()
    {
        var connection = GetDbConnection();
        
        var history = connection.Query<MigrationHistoric>(
            "SELECT Version as Id, AppliedOn, Description as Name FROM VersionInfo ORDER BY Version");
        
        ReturnDbConnection(connection);
        
        return history.ToList();
    }

    public void CloseAllConnections()
    {
        while (connections.TryPop(out var conn)) conn.Dispose();
    }

    public void Dispose()
    {
        CloseAllConnections();
        GC.SuppressFinalize(this);
    }
}