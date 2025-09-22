using System.Collections.Concurrent;
using System.Data;
using Dapper;
using Inventoria.Core.Domain.Abstractions;
using Inventoria.Core.Domain.Database;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

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
        var filesInFolder = Directory.GetFiles("Migrations", "*.sql").Order();

        var migrationHistory = CheckHistory();

        var connection = GetDbConnection();

        foreach (var file in filesInFolder)
        {
            if (migrationHistory.Any(m => m.Name == Path.GetFileName(file)))
                continue;

            var script = File.ReadAllText(file);
            using var command = connection.CreateCommand();
            command.CommandText = script;
            command.ExecuteNonQuery();

            connection.Execute("INSERT INTO MigrationHistory (Name, AppliedOn) VALUES (@Name, @AppliedOn)",
                new { Name = Path.GetFileName(file), AppliedOn = DateTime.UtcNow.ToString() });
        }

        ReturnDbConnection(connection);
    }

    public IEnumerable<MigrationHistoric> CheckHistory()
    {
        var connection = GetDbConnection();

        using (var command = connection.CreateCommand())
        {
            command.CommandText =
                "CREATE TABLE IF NOT EXISTS MigrationHistory ( Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT, AppliedOn TEXT)";

            command.ExecuteNonQuery();
        }

        ReturnDbConnection(connection);

        return [.. connection.Query<MigrationHistoric>("SELECT * FROM MigrationHistory")];
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