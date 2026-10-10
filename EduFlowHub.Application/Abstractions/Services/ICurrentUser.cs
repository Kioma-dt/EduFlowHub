namespace EduFlowHub.Application.Abstractions.Services;

public interface ICurrentUser
{
    Guid CurrentUserId { get; }
}