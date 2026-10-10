namespace EduFlowHub.Infrastructure.FileServices.Exceptions;

public class InvalidStorageFilePath
    : FileStorageException
{
    public InvalidStorageFilePath(string filePath)
        : base($"Storage file path '{filePath}' is invalid")
    {
    }
}