using EduFlowHub.Application.Features.User.Dtos;

namespace EduFlowHub.Application.Features.User.Queries.GetById;

public sealed record GetUserByIdQuery(Guid UserId)
    : IRequest<UserDto>;