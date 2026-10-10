namespace EduFlowHub.Application.Features.CourseEnrollment.Access.Policy;

public interface ICourseEnrollmentAccessPolicy
{
    bool CanEnroll(Domain.Entities.CourseEnrollment enrollment);
    bool CanUnEnroll(Domain.Entities.CourseEnrollment enrollment);
}