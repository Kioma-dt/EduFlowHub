using EduFlowHub.Application.Features.User.Commands.Create;
using EduFlowHub.Application.Features.User.Dtos;

namespace EduFlowHub.Application.Features.User.Commands.Delete;

public sealed record DeleteUserCommand(Guid UserId)
    : IRequest;