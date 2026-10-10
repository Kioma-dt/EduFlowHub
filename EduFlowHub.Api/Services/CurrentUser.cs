using System.Security.Claims;
using EduFlowHub.Application.Abstractions.Services.User;

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
                    "Current user id is missing or invalid");
            }

            return parsedUserId;
        }
    }

    public UserRole CurrentUserRole
    {
        get
        {
            string? userRole = _httpContextAccessor
                .HttpContext?
                .User
                .FindFirstValue(ClaimTypes.Role);
            
            if (!Enum.TryParse<UserRole>(userRole, out var parsedUserId))
            {
                throw new UnauthorizedUserException(
                    "Current user role is missing or invalid");
            }
            
            return parsedUserId;
        }
    }
}