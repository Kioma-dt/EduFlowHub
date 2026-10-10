namespace EduFlowHub.Application.Features.Authentication.Exceptions;

public class UsernameNotFoundException
    : ApplicationValidationException
{
    public UsernameNotFoundException(string username)
        : base($"User with username {username} not found")
    {
    }
}