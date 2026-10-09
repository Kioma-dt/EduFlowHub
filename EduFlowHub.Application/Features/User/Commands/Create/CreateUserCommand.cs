using EduFlowHub.Application.Abstractions.Persistence.Repositories;
using EduFlowHub.Application.Features.Course.Commands.Create;
using EduFlowHub.Application.Features.Course.Dtos;
using EduFlowHub.Application.Features.User.Dtos;
using MediatR.Pipeline;

namespace EduFlowHub.Application.Features.User.Commands.Create;

public sealed record CreateUserCommand(
    string Username,
    string Password,
    UserRole Role,
    string FirstName,
    string LastName)
    : IRequest<UserDto>;