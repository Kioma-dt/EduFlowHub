using EduFlowHub.Application.Features.User.Dtos;

namespace EduFlowHub.Application.Features.User.Commands.Create;

public sealed record CreateUserCommand(
    string Username,
    string Password,
    UserRole Role,
    string FirstName,
    string LastName)
    : IRequest<UserDto>;