using EduFlowHub.Application.Abstractions.Persistence.Repositories;

namespace EduFlowHub.Persistence.Repositories;

public class CourseEnrollmentRepository
    : ICourseEnrollmentRepository
{
    protected readonly DbContext DbContext;
    protected readonly DbSet<CourseEnrollment> CourseEnrollments;

    public CourseEnrollmentRepository(DbContext dbContext)
    {
        DbContext = dbContext;
        CourseEnrollments = dbContext.Set<CourseEnrollment>();
    }
    
    public async Task<CourseEnrollment?> GetByIdAsync(
        Guid studentId, 
        Guid courseId,
        CancellationToken cancellationToken)
    {
        return await CourseEnrollments.FirstOrDefaultAsync(enrollment => 
            enrollment.StudentId.Equals(studentId) 
            && enrollment.CourseId.Equals(courseId), 
            cancellationToken);
    }

    public async Task<IEnumerable<CourseEnrollment>> GetAllAsync(CancellationToken  cancellationToken = default)
    {
        return await CourseEnrollments.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<CourseEnrollment>> GetByCourseIdAsync(Guid courseId, CancellationToken cancellationToken = default)
    {
        return await CourseEnrollments
            .Include(enrollment => enrollment.Student)
            .Include(enrollment => enrollment.Course)
            .Where(enrollment => enrollment.CourseId.Equals(courseId))
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<CourseEnrollment>> GetByStudentIdAsync(Guid studentId, CancellationToken cancellationToken = default)
    {
        return await CourseEnrollments
            .Include(enrollment => enrollment.Student)
            .Include(enrollment => enrollment.Course)
            .Where(enrollment => enrollment.StudentId.Equals(studentId))
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid? courseId, Guid? studentId, CancellationToken cancellationToken = default)
    {
        var query = CourseEnrollments.AsQueryable();

        if (!courseId.HasValue)
        {
            query = query.Where(enrollment => enrollment.CourseId.Equals(courseId));
        }
        
        if (!studentId.HasValue)
        {
            query = query.Where(enrollment => enrollment.StudentId.Equals(studentId));
        }
        
        return await query.AnyAsync(cancellationToken);
    }

    public Task InsertAsync(CourseEnrollment courseEnrollment, CancellationToken cancellationToken = default)
    {
        CourseEnrollments.Add(courseEnrollment);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(CourseEnrollment courseEnrollment, CancellationToken  cancellationToken = default)
    {
        if (DbContext.Entry(courseEnrollment).State == EntityState.Detached)
        {
            CourseEnrollments.Attach(courseEnrollment);
        }
        CourseEnrollments.Remove(courseEnrollment);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid studentId, Guid courseId, CancellationToken cancellationToken = default)
    {
        CourseEnrollment? courseEnrollment = await CourseEnrollments.FirstOrDefaultAsync(enrollment => 
                enrollment.StudentId.Equals(studentId) 
               && enrollment.CourseId.Equals(courseId));

        if (courseEnrollment is not null)
        {
            await DeleteAsync(courseEnrollment, cancellationToken);
        }
    }
}