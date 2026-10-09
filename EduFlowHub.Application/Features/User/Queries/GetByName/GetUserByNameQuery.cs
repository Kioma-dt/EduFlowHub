using EduFlowHub.Application.Features.User.Dtos;
using EduFlowHub.Application.Features.User.Queries.GetById;

namespace EduFlowHub.Application.Features.User.Queries.GetByName;

public sealed record GetUserByNameQuery(
    string UserName)
    : IRequest<UserDto>;