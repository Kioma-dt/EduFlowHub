using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Abstractions.Services.User;

namespace EduFlowHub.Application.Features.CourseEnrollment.Access.Service;

public interface ICourseEnrollmentAccessService
{
    void EnsureCanEnroll(Guid studentId);
    void EnsureCanUnEnroll(Domain.Entities.CourseEnrollment courseEnrollment);
}