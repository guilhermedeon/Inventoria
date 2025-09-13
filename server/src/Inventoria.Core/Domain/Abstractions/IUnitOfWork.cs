using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventoria.Core.Domain.Abstractions;

public interface IUnitOfWork : IDisposable
{
    public void BeginTransaction();
    public void Commit();
    public void Rollback();

}
