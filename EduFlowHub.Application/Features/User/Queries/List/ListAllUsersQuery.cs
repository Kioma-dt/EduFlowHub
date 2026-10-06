using EduFlowHub.Application.Features.User.Dtos;

namespace EduFlowHub.Application.Features.User.Queries.List;

public sealed record ListAllUsersQuery()
    : IRequest<IEnumerable<UserDto>>;