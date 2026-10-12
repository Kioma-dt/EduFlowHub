namespace EduFlowHub.Application.Exceptions.NotFoundExceptions;

public class NotFoundException
    : Exception
{
    public  NotFoundException(string message) 
        : base(message)
    {
    }
}