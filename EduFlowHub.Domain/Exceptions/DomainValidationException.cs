namespace EduFlowHub.Domain.Exceptions;

public class DomainValidationException
    : Exception
{
    public  DomainValidationException(string validationMessage)
        :base(validationMessage)
    {
    }
}