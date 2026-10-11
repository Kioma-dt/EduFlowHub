using EduFlowHub.Application.Abstractions.Services.User;

namespace EduFlowHub.Application.Features.Course.Access.Policy;

public class CourseAccessPolicy
    : ICourseAccessPolicy
{
    private readonly ICurrentUser _currentUser;
    
    public CourseAccessPolicy(ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }

    public bool CanView(Domain.Entities.Course course) =>
        course.Status == CourseStatus.Published
        || _currentUser.CurrentUserRole == UserRole.Admin
        || course.AuthorId == _currentUser.CurrentUserId;

    public bool CanCreate(Guid authorId) =>
        _currentUser.CurrentUserRole == UserRole.Admin
        || authorId == _currentUser.CurrentUserId;

    public bool CanArchive(Domain.Entities.Course course) =>
        _currentUser.CurrentUserRole == UserRole.Admin
        || course.AuthorId == _currentUser.CurrentUserId;

    public bool CanPublish(Domain.Entities.Course course) =>
        _currentUser.CurrentUserRole == UserRole.Admin
        || course.AuthorId == _currentUser.CurrentUserId;

    public bool CanUnpublish(Domain.Entities.Course course) =>
        _currentUser.CurrentUserRole == UserRole.Admin;
    
    public bool CanRestore(Domain.Entities.Course course) =>
        _currentUser.CurrentUserRole == UserRole.Admin;

    public bool CanChangeAuthor(Domain.Entities.Course course) =>
        _currentUser.CurrentUserRole == UserRole.Admin;

    public bool CanEditDetails(Domain.Entities.Course course) =>
        _currentUser.CurrentUserRole == UserRole.Admin
        || (course.AuthorId == _currentUser.CurrentUserId);

    public bool CanDelete(Domain.Entities.Course course) =>
        _currentUser.CurrentUserRole == UserRole.Admin
        || (course.AuthorId == _currentUser.CurrentUserId);
}