using EduFlowHub.Application.Features.Authentication.Dtos;
using EduFlowHub.Application.Features.User.Dtos;

namespace EduFlowHub.Application.Features.Authentication.Commands.Register;

public sealed record RegisterCommand(
    string Username,
    string Password,
    UserRole Role,
    string FirstName,
    string LastName)
    : IRequest<UserDto>;