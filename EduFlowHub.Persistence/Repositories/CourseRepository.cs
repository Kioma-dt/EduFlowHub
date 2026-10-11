using System.Linq.Expressions;
using EduFlowHub.Application.Abstractions.Persistence.Repositories;
using EduFlowHub.Persistence.Repositories.Generic;

namespace EduFlowHub.Persistence.Repositories;

public class CourseRepository
    : Repository<Course>, 
        ICourseRepository
        
{
    public CourseRepository(DbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<Course?> GetByIdForAuthorAsync(
        Guid courseId,
        Guid authorId, 
        CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(course => course.Author)
            .Include(course => course.Category)
            .FirstOrDefaultAsync(course => (course.Id.Equals(courseId) 
                                            && course.AuthorId.Equals(authorId)), cancellationToken);
    }
    
    public async Task<IEnumerable<Course>> GetEnrolledCoursesAsync(
        Guid studentId, Expression<Func<Course, bool>>? filter = null, 
        Func<IQueryable<Course>, IOrderedQueryable<Course>>? orderBy = null, 
        int page = 1,
        int pageSize = 10,
        CancellationToken  cancellationToken = default)
    {
        var query = DbContext.Set<CourseEnrollment>()
                .Include(enrollment => enrollment.Course)
                .ThenInclude(course => course.Author)
                .Include(enrollment => enrollment.Course)
                .ThenInclude(course => course.Category)
                .Where(enrollment => enrollment.StudentId.Equals(studentId))
                .Select(enrollment => enrollment.Course)
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
}