using EduFlowHub.Application.Abstractions.Services.User;

namespace EduFlowHub.Application.Features.User.Access.Policy;

public class UserAccessPolicy
    : IUserAccessPolicy
{
    private readonly ICurrentUser _currentUser;
    
    public UserAccessPolicy(ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }

    public bool CanView(Domain.Entities.User user) =>
        _currentUser.CurrentUserRole ==  UserRole.Admin
        || _currentUser.CurrentUserId == user.Id;

    public bool CanEditDetails(Domain.Entities.User user) =>
        _currentUser.CurrentUserRole ==  UserRole.Admin
        || _currentUser.CurrentUserId == user.Id;

    public bool CanChangePasswordWithoutOldPassword(Domain.Entities.User user) =>
        _currentUser.CurrentUserRole ==  UserRole.Admin 
        && _currentUser.CurrentUserId != user.Id;

    public bool CanChangePassword(Domain.Entities.User user) =>
        _currentUser.CurrentUserRole ==  UserRole.Admin
        || _currentUser.CurrentUserId == user.Id;

    public bool CanCreate() =>
        _currentUser.CurrentUserRole == UserRole.Admin;

    public bool CanDelete(Domain.Entities.User user) =>
        _currentUser.CurrentUserRole == UserRole.Admin
        && _currentUser.CurrentUserId != user.Id;

    public bool CanUnlock(Domain.Entities.User user) =>
        _currentUser.CurrentUserRole == UserRole.Admin
        && _currentUser.CurrentUserId != user.Id;
}