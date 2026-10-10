namespace EduFlowHub.Infrastructure.FileServices.Exceptions;

public class FileStorageException
    : Exception
{
    public FileStorageException(string message)
        : base(message)
    {
    }
}