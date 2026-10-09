using EduFlowHub.Application.Features.User.Dtos;
using EduFlowHub.Application.Features.User.Queries.GetByName;

namespace EduFlowHub.Application.Features.User.Queries.List;

public sealed record ListAllUsersQuery()
    : IRequest<IEnumerable<UserDto>>;