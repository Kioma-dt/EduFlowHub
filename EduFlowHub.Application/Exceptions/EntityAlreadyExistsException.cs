namespace EduFlowHub.Application.Exceptions;

public class EntityAlreadyExistsException
    : Exception
{
    public EntityAlreadyExistsException(
        string entityName,
        string propertyName, 
        string propertyValue)
        : base($"{entityName} with {propertyName} = {propertyValue} already exists")
    {
    }
}