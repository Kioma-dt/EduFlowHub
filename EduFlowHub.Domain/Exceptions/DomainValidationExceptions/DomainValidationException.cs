namespace EduFlowHub.Domain.Exceptions.DomainValidationExceptions;

public class DomainValidationException
    : Exception
{
    public  DomainValidationException(string message)
        :base(message)
    {
    }
}