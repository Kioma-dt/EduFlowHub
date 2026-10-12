namespace EduFlowHub.Application.Exceptions.ContentTooLarge;

public class ContentTooLargeException
    : Exception
{
    public ContentTooLargeException(string message)
        : base(message)
    {
        
    }
}