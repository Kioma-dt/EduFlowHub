namespace EduFlowHub.Application.Exceptions.ApplicationStateExceptions;

public class ApplicationStateException
    : Exception
{
    public ApplicationStateException(string message)
        : base(message)
    {
    }
}