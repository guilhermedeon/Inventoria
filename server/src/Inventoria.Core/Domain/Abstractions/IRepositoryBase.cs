using Inventoria.Core.Domain.Database;
using Inventoria.SharedKernel;

namespace Inventoria.Core.Domain.Abstractions;

public interface IRepositoryBase<T>
{
    Task<Result<T?>> GetByIdAsync(int id);
    Task<Result<IEnumerable<T>>> GetAllAsync();
    Task<Result<bool>> ExistsAsync(int id);
    Task<Result> AddAsync(T entity, UnitOfWork? unitOfWork = null);
    Task<Result> UpdateAsync(T entity, UnitOfWork? unitOfWork = null);
    Task<Result> DeleteByIdAsync(int id, UnitOfWork? unitOfWork = null);
}