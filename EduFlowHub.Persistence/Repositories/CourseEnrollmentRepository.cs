using EduFlowHub.Application.Abstractions.Persistence.Repositories;

namespace EduFlowHub.Persistence.Repositories;

public class CourseEnrollmentRepository
    : ICourseEnrollmentRepository
{
    private readonly DbContext _dbContext;
    private readonly DbSet<CourseEnrollment> _courseEnrollments;

    public CourseEnrollmentRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
        _courseEnrollments = dbContext.Set<CourseEnrollment>();
    }
    
    public async Task<CourseEnrollment?> GetByIdAsync(
        Guid studentId, 
        Guid courseId)
    {
        return await _courseEnrollments.FirstOrDefaultAsync(enrollment => 
            enrollment.StudentId.Equals(studentId) 
            && enrollment.CourseId.Equals(courseId));
    }

    public async Task<IEnumerable<CourseEnrollment>> GetAllAsync()
    {
        return await _courseEnrollments.ToListAsync();
    }

    public async Task<IEnumerable<CourseEnrollment>> GetByCourseIdAsync(Guid courseId)
    {
        return await _courseEnrollments
            .Include(enrollment => enrollment.Student)
            .Include(enrollment => enrollment.Course)
            .Where(enrollment => enrollment.CourseId.Equals(courseId))
            .ToListAsync();
    }

    public async Task<IEnumerable<CourseEnrollment>> GetByStudentIdAsync(Guid studentId)
    {
        return await _courseEnrollments
            .Include(enrollment => enrollment.Student)
            .Include(enrollment => enrollment.Course)
            .Where(enrollment => enrollment.StudentId.Equals(studentId))
            .ToListAsync();
    }

    public Task InsertAsync(CourseEnrollment courseEnrollment)
    {
        _courseEnrollments.Add(courseEnrollment);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(CourseEnrollment courseEnrollment)
    {
        if (_dbContext.Entry(courseEnrollment).State == EntityState.Detached)
        {
            _courseEnrollments.Attach(courseEnrollment);
        }
        _courseEnrollments.Remove(courseEnrollment);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid studentId, Guid courseId)
    {
        CourseEnrollment? courseEnrollment = await _courseEnrollments.FirstOrDefaultAsync(enrollment => 
                enrollment.StudentId.Equals(studentId) 
               && enrollment.CourseId.Equals(courseId));

        if (courseEnrollment is not null)
        {
            await DeleteAsync(courseEnrollment);
        }
    }
}