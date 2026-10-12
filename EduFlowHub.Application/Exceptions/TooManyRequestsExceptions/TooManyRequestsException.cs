namespace EduFlowHub.Application.Exceptions.TooManyRequestsExceptions;

public class TooManyRequestsException
    : Exception
{
    public TooManyRequestsException(string message)
        : base(message)
    {
    }
}