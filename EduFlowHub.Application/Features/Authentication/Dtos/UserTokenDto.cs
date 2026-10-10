using EduFlowHub.Application.Features.User.Dtos;

namespace EduFlowHub.Application.Features.Authentication.Dtos;

public sealed record UserTokenDto(
    string Token,
    UserDto User);