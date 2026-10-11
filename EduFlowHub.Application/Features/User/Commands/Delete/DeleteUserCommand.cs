namespace EduFlowHub.Application.Features.User.Commands.Delete;

public sealed record DeleteUserCommand(Guid UserId)
    : IRequest;