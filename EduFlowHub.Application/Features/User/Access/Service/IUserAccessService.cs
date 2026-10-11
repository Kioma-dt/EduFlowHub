namespace EduFlowHub.Application.Features.User.Access.Service;

public interface IUserAccessService
{
    void EnsureCanChangePassword(Domain.Entities.User user);
    void EnsureCanChangePasswordWithoutOldPassword(Domain.Entities.User user);
    void EnsureCanCreate();
    void EnsureCanDelete(Domain.Entities.User user);
    void EnsureCanUpdate(Domain.Entities.User user);
}