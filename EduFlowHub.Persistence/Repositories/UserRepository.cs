using System.Linq.Expressions;
using EduFlowHub.Application.Abstractions.Persistence.Repositories;
using EduFlowHub.Persistence.Repositories.Generic;

namespace EduFlowHub.Persistence.Repositories;

public class UserRepository
    : Repository<User>,
        IUserRepository
        
{
    public UserRepository(DbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<User?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(u => u.Name == name, cancellationToken);
    }
    
    public async Task<IEnumerable<User>> GetEnrolledOnCourse(Guid courseId, 
        Expression<Func<User, bool>>? filter = null, 
        Func<IQueryable<User>, IOrderedQueryable<User>>? orderBy = null,
        int page = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var query = DbContext.Set<CourseEnrollment>()
            .Include(enrollment => enrollment.Student)
            .Where(enrollment => enrollment.CourseId.Equals(courseId))
            .Select(enrollment => enrollment.Student!)
            .AsQueryable();

        if (filter is not null)
        {
            query = query.Where(filter);
        }
        
        int skip = (page - 1) * pageSize;
        int take = pageSize;
        
        if (orderBy is not null)
        {
            return await orderBy(query)
                .Skip(skip)
                .Take(take)
                .ToListAsync(cancellationToken);
        }
        else
        {
            return await query.OrderBy(course => course.Id).
                Skip(skip)
                .Take(take)
                .ToListAsync(cancellationToken);
        }
    }

    public async Task<bool> AnyByRoleAsync(UserRole role, CancellationToken cancellationToken = default)
    {
        return await DbSet.AnyAsync(user => user.Role == role, cancellationToken);
    }
}