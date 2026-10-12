namespace EduFlowHub.Application.Exceptions.UnauthorizedExceptions;

public class UnauthorizedException
    : Exception
{
    public  UnauthorizedException(string message)
        : base(message)
    {
    }
}