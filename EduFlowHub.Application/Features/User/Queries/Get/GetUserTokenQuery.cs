namespace EduFlowHub.Application.Features.User.Queries.Get;

public sealed record GetUserTokenQuery(
    Guid UserId)
    : IRequest<string>;