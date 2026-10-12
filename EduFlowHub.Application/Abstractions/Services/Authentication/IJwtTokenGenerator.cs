namespace EduFlowHub.Application.Abstractions.Services.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateJwtToken(
        Guid userId, 
        UserRole role);
}