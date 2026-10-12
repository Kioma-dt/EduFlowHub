namespace EduFlowHub.Domain.Exceptions.DomainStateException;

public class DomainStateException
    : Exception
{
    public DomainStateException(string message)
        : base(message)
    {
    }
}