namespace EduFlowHub.Application.Abstractions.Persistence.Repositories;

public interface ICourseEnrollmentRepository
{
    Task<bool> ExistsAsync(
        Guid studentId, 
        Guid courseId);
    
    Task InsertAsync(CourseEnrollment courseEnrollment);
    Task DeleteAsync(CourseEnrollment courseEnrollment);
    Task DeleteAsync(
        Guid studentId, 
        Guid courseId);
}