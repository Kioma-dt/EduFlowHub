using EduFlowHub.Application.Abstractions.Services.User;

namespace EduFlowHub.Application.Features.CourseEnrollment.Access.Policy;

public class CourseEnrollmentAccessPolicy
    : ICourseEnrollmentAccessPolicy
{
    private readonly ICurrentUser _currentUser;

    public CourseEnrollmentAccessPolicy(ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }
    
    public bool CanEnroll(Guid studentId) =>
        (_currentUser.CurrentUserRole ==  UserRole.Admin 
         || (_currentUser.CurrentUserRole == UserRole.Student
             && _currentUser.CurrentUserId == studentId));

    public bool CanUnEnroll(Domain.Entities.CourseEnrollment enrollment) =>
        (_currentUser.CurrentUserRole ==  UserRole.Admin 
         || (_currentUser.CurrentUserRole == UserRole.Student
             && _currentUser.CurrentUserId == enrollment.StudentId));
}