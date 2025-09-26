using Inventoria.Core.Domain.Database;

namespace Inventoria.Core.Domain.Abstractions;

public interface IRepositoryBase<T>
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<bool> ExistsAsync(int id);
    Task AddAsync(T entity, UnitOfWork? unitOfWork = null);
    Task UpdateAsync(T entity, UnitOfWork? unitOfWork = null);
    Task DeleteByIdAsync(int id, UnitOfWork? unitOfWork = null);
}