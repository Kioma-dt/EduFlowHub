using System.Linq.Expressions;
using EduFlowHub.Application.Abstractions.Persistence.Repositories.Generic;
using EduFlowHub.Domain.Entities.Abstractions;

namespace EduFlowHub.Persistence.Repositories.Generic;

public class Repository<T>
    : IRepository<T>
    where T : Entity
{
    readonly DbContext _dbContext;
    readonly DbSet<T> _dbSet;
    
    public Repository(DbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }
    
    public virtual async Task<IEnumerable<T>> GetAllAsync(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, 
        params string[] includeProperties)
    {
        IQueryable<T> query = _dbSet;

        if (filter is not null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }

        if (orderBy is not null)
        {
            return await orderBy(query).ToListAsync();
        }
        else
        {
            return await query.ToListAsync();
        }
    }

    public virtual async Task<T?> GetByIdAsync(
        Guid id, 
        params string[] includeProperties)
    {
        IQueryable<T> query = _dbSet;

        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }

        return await query.FirstOrDefaultAsync(entity => id.Equals(entity.Id));
    }

    public virtual async Task InsertAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public virtual Task UpdateAsync(T entity)
    {
        _dbSet.Attach(entity);
        _dbContext.Entry(entity).State = EntityState.Modified;
        return Task.CompletedTask;
    }

    public virtual Task DeleteAsync(T entity)
    {
        if (_dbContext.Entry(entity).State == EntityState.Detached)
        {
            _dbSet.Attach(entity);
        }
        _dbSet.Remove(entity);
        return Task.CompletedTask;
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        T? entityToDelete = await _dbSet.FirstOrDefaultAsync(entity => entity.Id.Equals(id));

        if (entityToDelete is not null)
        {
            await DeleteAsync(entityToDelete);
        }
    }
}