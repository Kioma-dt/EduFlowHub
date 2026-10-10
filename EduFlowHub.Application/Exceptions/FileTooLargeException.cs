namespace EduFlowHub.Application.Exceptions;

public class FileTooLargeException
    : Exception
{
    public FileTooLargeException(int maxFileSize)
        : base($"File too large. Max Size: {maxFileSize}")
    {
    }
}