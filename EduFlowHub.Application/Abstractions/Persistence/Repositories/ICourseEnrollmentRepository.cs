namespace EduFlowHub.Application.Abstractions.Persistence.Repositories;

public interface ICourseEnrollmentRepository
{
    Task<CourseEnrollment?> GetByIdAsync(
        Guid studentId, 
        Guid courseId,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<CourseEnrollment>> GetAllAsync(CancellationToken cancellationToken = default);
    
    Task<IEnumerable<CourseEnrollment>> GetByCourseIdAsync(Guid courseId, CancellationToken cancellationToken = default);
    Task<IEnumerable<CourseEnrollment>> GetByStudentIdAsync(Guid studentId, CancellationToken cancellationToken = default);
    
    Task InsertAsync(CourseEnrollment courseEnrollment, CancellationToken cancellationToken = default);
    Task DeleteAsync(CourseEnrollment courseEnrollment, CancellationToken cancellationToken = default);
    Task DeleteAsync(
        Guid studentId, 
        Guid courseId,
        CancellationToken cancellationToken = default);
}