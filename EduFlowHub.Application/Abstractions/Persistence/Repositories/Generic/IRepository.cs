using System.Linq.Expressions;
using EduFlowHub.Domain.Entities.Abstractions;

namespace EduFlowHub.Application.Abstractions.Persistence.Repositories.Generic;

public interface IRepository<T>
    where T : Entity
{
    public Task<IEnumerable<T>> GetAllAsync(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        CancellationToken cancellationToken = default,
        params Expression<Func<T, object>>[] includes);
    
    public Task<T?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default,
        params Expression<Func<T, object>>[] includes);

    public Task<bool> ExistsAsync(Expression<Func<T, bool>>? filter = null, CancellationToken cancellationToken = default);
    public Task<long> CountAsync(Expression<Func<T, bool>>? filter = null, CancellationToken cancellationToken = default);
    
    public Task InsertAsync(T entity, CancellationToken cancellationToken = default);
    public Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    public Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
    public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}