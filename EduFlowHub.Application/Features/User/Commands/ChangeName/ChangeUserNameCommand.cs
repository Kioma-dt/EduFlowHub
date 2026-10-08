using EduFlowHub.Application.Features.User.Commands.Update;
using EduFlowHub.Application.Features.User.Dtos;

namespace EduFlowHub.Application.Features.User.Commands.ChangeName;

public sealed record ChangeUserNameCommand(
    Guid UserId,
    string UserName)
    : IRequest<UserDto>;