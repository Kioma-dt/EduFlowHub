namespace EduFlowHub.Application.Exceptions.ApplicationStateExceptions;

public class EntityAlreadyExistsException
    : ApplicationStateException
{
    public EntityAlreadyExistsException(
        string entityName,
        string propertyName, 
        string propertyValue)
        : base($"{entityName} with {propertyName} = {propertyValue} already exists")
    {
    }
}