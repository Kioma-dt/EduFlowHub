namespace EduFlowHub.Application.Exceptions;

public class AccessDeniedException
    : Exception
{
    public  AccessDeniedException(string operation)
        : base($"You do not have permission to {operation}")
    {
    }
}