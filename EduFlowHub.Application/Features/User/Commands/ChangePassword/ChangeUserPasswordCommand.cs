using EduFlowHub.Application.Features.User.Commands.Create;
using EduFlowHub.Application.Features.User.Dtos;

namespace EduFlowHub.Application.Features.User.Commands.ChangePassword;

public sealed record ChangeUserPasswordCommand(
    Guid UserId,
    string Password)
    : IRequest<UserDto>;