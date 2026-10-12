namespace EduFlowHub.Application.Exceptions.ApplicationStateExceptions;

public class CanNotDeleteEntityException
    : ApplicationStateException
{
    public CanNotDeleteEntityException(string message)
        : base(message)
    {
    }
}