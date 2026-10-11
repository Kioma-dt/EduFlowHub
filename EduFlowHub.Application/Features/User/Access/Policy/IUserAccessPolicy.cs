namespace EduFlowHub.Application.Features.User.Access.Policy;

public interface IUserAccessPolicy
{
    bool CanView(Domain.Entities.User user);
    bool CanEditDetails(Domain.Entities.User user);
    bool CanChangePasswordWithoutOldPassword(Domain.Entities.User user);
    bool CanChangePassword(Domain.Entities.User user);
    bool CanCreate();
    bool CanDelete(Domain.Entities.User user);
}