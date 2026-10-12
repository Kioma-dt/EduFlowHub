namespace EduFlowHub.Application.Exceptions.AccessDeniedExceptions;

public class AccessDeniedException
    : Exception
{
    public  AccessDeniedException(string operation)
        : base($"You do not have permission to {operation}")
    {
    }
}