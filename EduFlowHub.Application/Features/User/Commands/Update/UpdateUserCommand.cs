namespace EduFlowHub.Application.Features.User.Commands.Update;

public sealed record UpdateUserCommand(Guid UserId, string? FirstName, string? LastName)
    : IRequest;