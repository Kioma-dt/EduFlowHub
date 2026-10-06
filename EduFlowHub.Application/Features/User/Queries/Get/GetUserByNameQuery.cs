using EduFlowHub.Application.Features.User.Dtos;

namespace EduFlowHub.Application.Features.User.Queries.Get;

public sealed record GetUserByNameQuery(
    Guid UserName)
    : IRequest<UserDto>;