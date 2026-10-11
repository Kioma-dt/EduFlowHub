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

    public async Task<IEnumerable<Course>> GetEnrolledCoursesAsync(Guid studentId, Expression<Func<Course, bool>> filter, CancellationToken  cancellationToken = default)
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
}