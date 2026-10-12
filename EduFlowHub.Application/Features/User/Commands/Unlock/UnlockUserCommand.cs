namespace EduFlowHub.Application.Features.User.Commands.Unlock;

public sealed record UnlockUserCommand(Guid UserId) 
    : IRequest;