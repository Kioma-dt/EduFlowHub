using EduFlowHub.Application.Features.Dtos;
using EduFlowHub.Application.Features.User.Dtos;
using EduFlowHub.Application.Features.User.Queries.GetByName;

namespace EduFlowHub.Application.Features.User.Queries.ListAll;

public sealed record ListAllUsersQuery(int Page = 1, int PageSize = 10)
    : IRequest<PagedDto<UserDto>>;