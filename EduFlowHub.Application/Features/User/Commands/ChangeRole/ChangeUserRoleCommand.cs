using EduFlowHub.Application.Features.User.Commands.Update;
using EduFlowHub.Application.Features.User.Dtos;

namespace EduFlowHub.Application.Features.User.Commands.ChangeRole;

public sealed record ChangeUserRoleCommand(
    Guid UserId,
    UserRole UserRole)
    : IRequest<UserDto>;