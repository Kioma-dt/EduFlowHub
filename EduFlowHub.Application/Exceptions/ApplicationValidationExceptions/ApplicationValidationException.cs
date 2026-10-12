namespace EduFlowHub.Application.Exceptions.ApplicationValidationExceptions;

public class ApplicationValidationException
    : Exception
{
    public  ApplicationValidationException(string message)
        : base(message)
    {
    }
}