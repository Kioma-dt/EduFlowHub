namespace EduFlowHub.Application.Exceptions;

public class EntityNotFoundException
    : Exception
{
    public EntityNotFoundException(string entityName, Guid entityId)
        : base($"{entityName} does not exists with id {entityId}")
    {
    }
}