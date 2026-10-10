namespace EduFlowHub.Domain.Exceptions;

public class DomainWrongStatusException
    : Exception
{
    public DomainWrongStatusException(string message)
        : base(message)
    {
    }
}