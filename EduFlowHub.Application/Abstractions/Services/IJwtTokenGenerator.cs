using EduFlowHub.Application.Features.Authentication.Queries.Login;

namespace EduFlowHub.Application.Abstractions.Services;

public interface IJwtTokenGenerator
{
    string GenerateJwtToken(
        Guid userId, 
        UserRole role);
}