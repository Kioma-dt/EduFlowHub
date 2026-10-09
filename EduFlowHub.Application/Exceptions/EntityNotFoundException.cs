namespace EduFlowHub.Application.Exceptions;

public class EntityNotFoundException
    : Exception
{
    public EntityNotFoundException(
        string entityName, 
        string propertyName, 
        string propertyValue)
        : base($"{entityName} does not exists with {propertyName} = {propertyValue}")
    {
    }
}