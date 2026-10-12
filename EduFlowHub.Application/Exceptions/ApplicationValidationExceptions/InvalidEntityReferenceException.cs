namespace EduFlowHub.Application.Exceptions.ApplicationValidationExceptions;

public class InvalidEntityReferenceException
    : ApplicationException
{
    public InvalidEntityReferenceException(
        string entityName,
        Guid entityId)
        : base($"Referenced {entityName} with id '{entityId}' does not exist")
    {
    }

}