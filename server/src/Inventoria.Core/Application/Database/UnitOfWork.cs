using Inventoria.Core.Domain.Abstractions;
using System.Data;

namespace Inventoria.Core.Application.Database;

public class UnitOfWork(IInventoriaDatabase database) : IUnitOfWork
{
    ~UnitOfWork()
    {
        Dispose();
    }

    public IDbConnection? Connection { get; private set; }
    public IDbTransaction? Transaction { get; private set; }
    public IDbTransaction BeginTransaction()
    {
        CheckConnetion();

        Transaction = Connection!.BeginTransaction();

        return Transaction;
    }

    public void Commit()
    {
        Transaction?.Commit();

        Dispose();
    }

    public void Dispose()
    {
        Transaction?.Dispose();
        if (Connection is not null)
        {
            database.ReturnDbConnection(Connection);
            Connection = null;
        }
    }

    public void Rollback()
    {
        Transaction?.Rollback();

        Dispose();
    }

    private void CheckConnetion()
    {
        Connection ??= database.GetDbConnection();

        if (Connection.State != ConnectionState.Open)
            Connection.Open();

    }
}
