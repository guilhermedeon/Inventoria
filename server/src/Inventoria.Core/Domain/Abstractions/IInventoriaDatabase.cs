using Inventoria.Core.Domain.Entities.Migrations;
using System.Data;

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