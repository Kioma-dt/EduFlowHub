using EduFlowHub.Application.Features.Authentication.Dtos;

namespace EduFlowHub.Application.Features.Authentication.Commands.Login;

public record LoginCommand(string Username, string Password)
    : IRequest<UserTokenDto>;