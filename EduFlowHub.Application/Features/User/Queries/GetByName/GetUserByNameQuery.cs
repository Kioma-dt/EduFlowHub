using EduFlowHub.Application.Features.User.Dtos;

namespace EduFlowHub.Application.Features.User.Queries.GetByName;

public sealed record GetUserByNameQuery(string UserName)
    : IRequest<UserDto>;