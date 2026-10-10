namespace EduFlowHub.Application.Features.User.Exceptions;

public class WrongPasswordException
    : ApplicationValidationException
{
    public  WrongPasswordException() 
        : base("Wrong password")
    {
    }
}