using System.Data;
using Inventoria.Core.Domain.Database;

namespace Inventoria.Core.Domain.Abstractions;

public interface IInventoriaDatabase : IDisposable
{
    void ApplyMigrations();
    IEnumerable<MigrationHistoric> CheckHistory();
    IDbConnection CreateDbConnection();
    IDbConnection GetDbConnection();
    void ReturnDbConnection(IDbConnection connection);
    void CloseAllConnections();
}