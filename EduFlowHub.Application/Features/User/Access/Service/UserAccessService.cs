using EduFlowHub.Application.Features.User.Access.Policy;

namespace EduFlowHub.Application.Features.User.Access.Service;

public class UserAccessService
    : IUserAccessService
{
    private readonly IUserAccessPolicy _userAccessPolicy;

    public UserAccessService(IUserAccessPolicy userAccessPolicy)
    {
        _userAccessPolicy = userAccessPolicy;
    }
    
    public void EnsureCanChangePassword(Domain.Entities.User user)
    {
        if (!_userAccessPolicy.CanChangePassword(user)
            || !_userAccessPolicy.CanView(user))
        {
            throw new AccessDeniedException("Change user password");
        }
    }

    public void EnsureCanChangePasswordWithoutOldPassword(Domain.Entities.User user)
    {
        if (!_userAccessPolicy.CanChangePasswordWithoutOldPassword(user)
            || !_userAccessPolicy.CanView(user))
        {
            throw new AccessDeniedException("Change user password without old password");
        }
    }

    public void EnsureCanCreate()
    {
        if (!_userAccessPolicy.CanCreate())
        {
            throw new AccessDeniedException("Create user");
        }
    }

    public void EnsureCanDelete(Domain.Entities.User user)
    {
        if (!_userAccessPolicy.CanDelete(user)
            || !_userAccessPolicy.CanView(user))
        {
            throw new AccessDeniedException("Delete user");
        }
    }

    public void EnsureCanUpdate(Domain.Entities.User user)
    {
        if (!_userAccessPolicy.CanEditDetails(user)
            || !_userAccessPolicy.CanView(user))
        {
            throw new AccessDeniedException("Update user");
        }
    }

    public void EnsureCanUnlock(Domain.Entities.User user)
    {
        if (!_userAccessPolicy.CanEditDetails(user)
           || !_userAccessPolicy.CanUnlock(user))
        {
            throw new AccessDeniedException("Unlock user");
        }
    }
}