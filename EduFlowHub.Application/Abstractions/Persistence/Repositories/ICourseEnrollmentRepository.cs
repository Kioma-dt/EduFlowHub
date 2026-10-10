namespace EduFlowHub.Application.Abstractions.Persistence.Repositories;

public interface ICourseEnrollmentRepository
{
    Task<CourseEnrollment?> GetByIdAsync(
        Guid studentId, 
        Guid courseId);

    Task<IEnumerable<CourseEnrollment>> GetAllAsync();
    
    Task<IEnumerable<CourseEnrollment>> GetByCourseIdAsync(Guid courseId);
    Task<IEnumerable<CourseEnrollment>> GetByStudentIdAsync(Guid studentId);
    
    Task InsertAsync(CourseEnrollment courseEnrollment);
    Task DeleteAsync(CourseEnrollment courseEnrollment);
    Task DeleteAsync(
        Guid studentId, 
        Guid courseId);
}