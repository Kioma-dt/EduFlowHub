namespace EduFlowHub.Application.Features.CourseEnrollment.Access.Policy;

public interface ICourseEnrollmentAccessPolicy
{
    bool CanEnroll(Guid studentId);
    bool CanUnEnroll(Domain.Entities.CourseEnrollment enrollment);
}