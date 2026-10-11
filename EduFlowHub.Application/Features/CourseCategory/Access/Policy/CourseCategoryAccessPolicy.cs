using EduFlowHub.Application.Abstractions.Services.User;

namespace EduFlowHub.Application.Features.CourseCategory.Access.Policy;

public class CourseCategoryAccessPolicy
    : ICourseCategoryAccessPolicy
{
    private readonly ICurrentUser _currentUser;

    public CourseCategoryAccessPolicy(ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }
    
    public bool CanView(Domain.Entities.CourseCategory courseCategory) =>
        true;
    public bool CanEdit (Domain.Entities.CourseCategory courseCategory) =>
        _currentUser.CurrentUserRole ==  UserRole.Admin;
    public bool CanDelete(Domain.Entities.CourseCategory courseCategory) =>
        _currentUser.CurrentUserRole ==  UserRole.Admin;
    public bool CanCreate(Domain.Entities.CourseCategory courseCategory) =>
        _currentUser.CurrentUserRole ==  UserRole.Admin;
}