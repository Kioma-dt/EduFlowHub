namespace EduFlowHub.Application.Exceptions;

public class CanNotDeleteEntityException
    : Exception
{
    public CanNotDeleteEntityException(string message)
        : base(message)
    {
    }
}