namespace EduFlowHub.Application.Features.Authentication.Exceptions;

public class WrongPasswordException
    : ApplicationValidationException
{
    public WrongPasswordException()
        : base("Wrong password")
    {
    }
}