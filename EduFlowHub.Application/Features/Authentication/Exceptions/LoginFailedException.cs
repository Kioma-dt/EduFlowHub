namespace EduFlowHub.Application.Features.Authentication.Exceptions;

public class LoginFailedException
    : Exception
{
    public LoginFailedException() 
        : base("Wrong username or password")
    {
    }
}