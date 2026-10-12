namespace EduFlowHub.Application.Features.Authentication.Exceptions;

public class LoginFailedException
    : UnauthorizedAccessException
{
    public LoginFailedException() 
        : base("Wrong username or password")
    {
    }
}