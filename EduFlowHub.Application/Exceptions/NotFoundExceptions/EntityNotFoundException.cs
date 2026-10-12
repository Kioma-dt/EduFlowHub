namespace EduFlowHub.Application.Exceptions.NotFoundExceptions;

public class EntityNotFoundException
    : NotFoundException
{
    public EntityNotFoundException(
        string entityName, 
        string propertyName, 
        string propertyValue)
        : base($"{entityName} does not exists with {propertyName} = {propertyValue}")
    {
    }
}