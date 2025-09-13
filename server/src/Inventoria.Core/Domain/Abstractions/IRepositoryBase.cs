using System.Data;

namespace Inventoria.Core.Domain.Abstractions;

public interface IRepositoryBase<T>
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<bool> ExistsAsync(int id);
    Task AddAsync(T entity, IUnitOfWork unitOfWork);
    Task UpdateAsync(T entity, IUnitOfWork unitOfWork);
    Task DeleteAsync(T entity, IUnitOfWork unitOfWork);
}
