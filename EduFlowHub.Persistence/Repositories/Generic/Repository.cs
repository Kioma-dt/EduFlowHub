using System.Linq.Expressions;
using EduFlowHub.Application.Abstractions.Persistence.Repositories.Generic;
using EduFlowHub.Domain.Entities.Abstractions;

namespace EduFlowHub.Persistence.Repositories.Generic;

public abstract class Repository<T>
    : IRepository<T>
    where T : Entity
{
    protected readonly DbContext DbContext;
    protected readonly DbSet<T> DbSet;
    
    protected Repository(DbContext dbContext)
    {
        DbContext = dbContext;
        DbSet = DbContext.Set<T>();
    }
    
    public virtual async Task<IEnumerable<T>> GetAllAsync(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        CancellationToken  cancellationToken = default,
        params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = DbSet;

        if (filter is not null)
        {
            query = query.Where(filter);
        }

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        if (orderBy is not null)
        {
            return await orderBy(query).ToListAsync(cancellationToken);
        }
        else
        {
            return await query.ToListAsync(cancellationToken);
        }
    }

    public virtual async Task<T?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default,
        params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = DbSet;
        
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        
        return await query.FirstOrDefaultAsync(
            entity => id.Equals(entity.Id),
            cancellationToken);
    }

    public virtual async Task InsertAsync(T entity, CancellationToken cancellationToken = default)
    {
        await DbSet.AddAsync(entity, cancellationToken);
    }

    public virtual Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        DbSet.Attach(entity);
        DbContext.Entry(entity).State = EntityState.Modified;
        return Task.CompletedTask;
    }

    public virtual Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        if (DbContext.Entry(entity).State == EntityState.Detached)
        {
            DbSet.Attach(entity);
        }
        DbSet.Remove(entity);
        return Task.CompletedTask;
    }

    public virtual async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        T? entityToDelete = await DbSet.FirstOrDefaultAsync(entity => entity.Id.Equals(id), cancellationToken);

        if (entityToDelete is not null)
        {
            await DeleteAsync(entityToDelete, cancellationToken);
        }
    }
}