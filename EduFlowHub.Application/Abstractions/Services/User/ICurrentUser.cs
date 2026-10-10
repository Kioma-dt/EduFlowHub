namespace EduFlowHub.Application.Abstractions.Services.User;

public interface ICurrentUser
{
    Guid CurrentUserId { get; }
    UserRole CurrentUserRole { get; }
}