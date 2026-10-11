using EduFlowHub.Application.Features.CourseEnrollment.Access.Policy;

namespace EduFlowHub.Application.Features.CourseEnrollment.Access.Service;

public class CourseEnrollmentAccessService
    : ICourseEnrollmentAccessService
{
    private readonly ICourseEnrollmentAccessPolicy _courseEnrollmentAccessPolicy;

    public CourseEnrollmentAccessService(ICourseEnrollmentAccessPolicy courseEnrollmentAccessPolicy)
    {
        _courseEnrollmentAccessPolicy = courseEnrollmentAccessPolicy;
    }
    
    public void EnsureCanEnroll(Guid studentId)
    {
        if (!_courseEnrollmentAccessPolicy.CanEnroll(studentId))
        {
            throw new AccessDeniedException("Course enrollment");
        }
    }

    public void EnsureCanUnEnroll(Domain.Entities.CourseEnrollment courseEnrollment)
    {
        if (!_courseEnrollmentAccessPolicy.CanUnEnroll(courseEnrollment))
        {
            throw new AccessDeniedException("Course unenrollment");
        }
    }
}