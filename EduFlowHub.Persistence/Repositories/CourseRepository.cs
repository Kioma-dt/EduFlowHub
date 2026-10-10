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

    public async Task<IEnumerable<Course>> GetEnrolledCoursesAsync(Guid studentId, CancellationToken  cancellationToken = default)
    {
        return await DbContext.Set<CourseEnrollment>()
            .Include(enrollment => enrollment.Course)
            .ThenInclude(enrollment => enrollment.Author)
            .Include(enrollment => enrollment.Course)
            .ThenInclude(enrollment => enrollment.Category)
            .Where(enrollment => enrollment.StudentId.Equals(studentId))
            .Select(enrollment => enrollment.Course!)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Course>> GetWithFiltersAsync(
        Guid? categoryId,
        Guid? authorId, 
        CourseStatus? status,
        CancellationToken  cancellationToken = default)
    {
        IQueryable<Course> query = DbSet.AsQueryable();

        if (categoryId.HasValue)
        {
            query = query.Where(course => course.CategoryId.Equals(categoryId));
        }
        if (authorId.HasValue)
        {
            query = query.Where(course => course.AuthorId.Equals(authorId));
        }
        if (status.HasValue)
        {
            query = query.Where(course => course.Status.Equals(status));
        }
        
        return await query.ToListAsync(cancellationToken);
    }
}