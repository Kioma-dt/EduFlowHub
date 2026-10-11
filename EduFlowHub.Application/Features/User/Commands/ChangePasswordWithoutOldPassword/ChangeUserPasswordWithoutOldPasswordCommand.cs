namespace EduFlowHub.Application.Features.User.Commands.ChangePasswordWithoutOldPassword;

public sealed record ChangeUserPasswordWithoutOldPasswordCommand(Guid UserId, string Password)
    : IRequest;