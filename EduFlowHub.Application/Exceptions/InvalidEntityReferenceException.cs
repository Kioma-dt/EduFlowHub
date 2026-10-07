namespace EduFlowHub.Application.Exceptions;

public class InvalidEntityReferenceException
    : Exception
{
    public InvalidEntityReferenceException(
        string entityName,
        Guid entityId)
        : base($"Referenced {entityName} with id '{entityId}' does not exist")
    {
    }

}