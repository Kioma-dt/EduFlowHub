using EduFlowHub.Application.Features.Authentication.Dtos;

namespace EduFlowHub.Application.Features.Authentication.Queries.Login;

public record LoginQuery(string Username, string Password)
    : IRequest<UserTokenDto>;