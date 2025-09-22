using System.Data;

namespace Inventoria.Core.Domain.Database;

public interface IUnitOfWork
{
    IDbConnection? Connection { get; }
    IDbTransaction? Transaction { get; }

    IDbTransaction BeginTransaction();
    void Commit();
    void Dispose();
    void Rollback();
}