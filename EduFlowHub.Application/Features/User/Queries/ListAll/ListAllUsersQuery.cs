using EduFlowHub.Application.Features.User.Dtos;
using EduFlowHub.Application.Features.User.Queries.GetByName;

namespace EduFlowHub.Application.Features.User.Queries.ListAll;

public sealed record ListAllUsersQuery()
    : IRequest<IEnumerable<UserDto>>;