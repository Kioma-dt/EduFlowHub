namespace EduFlowHub.Application.Features.User.Commands.ChangePassword;

public sealed record ChangeUserPasswordCommand(Guid UserId, string Password, string OldPassword)
    : IRequest;