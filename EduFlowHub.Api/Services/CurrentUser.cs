using System.Security.Claims;
using EduFlowHub.Application.Abstractions.Services;

namespace EduFlowHub.Api.Services;

public class CurrentUser
    : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid CurrentUserId
    {
        get
        {
            string? userId = _httpContextAccessor
                .HttpContext?
                .User
                .FindFirstValue(ClaimTypes.NameIdentifier);
            
            if (!Guid.TryParse(userId, out var parsedUserId))
            {
                throw new UnauthorizedUserException(
                    "Current user id is missing or invalid.");
            }

            return parsedUserId;
        }
    }
}