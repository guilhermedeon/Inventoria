using System.Data;

namespace Inventoria.Core.Domain.Abstractions;

public interface IUnitOfWork : IDisposable
{
    public IDbTransaction BeginTransaction();
    public void Commit();
    public void Rollback();

}
