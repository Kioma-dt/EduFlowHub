using System.Linq.Expressions;
using EduFlowHub.Domain.Entities.Abstractions;

namespace EduFlowHub.Application.Abstractions.Persistence.Repositories.Generic;

public interface IRepository<T>
    where T : Entity
{
    public Task<IEnumerable<T>> GetAllAsync(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        params Expression<Func<T, object>>[] includes);
    
    public Task<T?> GetByIdAsync(
        Guid id,
        params Expression<Func<T, object>>[] includes);
    
    public Task InsertAsync(T entity);
    public Task UpdateAsync(T entity);
    public Task DeleteAsync(T entity);
    public Task DeleteAsync(Guid id);
}