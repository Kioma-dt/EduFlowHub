using EduFlowHub.Application.Features.User.Dtos;

namespace EduFlowHub.Application.Features.User.Commands.Update;

public sealed record UpdateUserCommand(
    Guid UserId,
    string? Username,
    string? Password,
    UserRole? Role,
    string? Email,
    string? Phone,
    string? FirstName,
    string? LastName)
    : IRequest<UserDto>;