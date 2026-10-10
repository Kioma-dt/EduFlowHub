namespace EduFlowHub.Infrastructure.FileServices.Exceptions;

public class UnsupportedFileFormatException
    : FileStorageException
{
    public  UnsupportedFileFormatException()
        : base($"Unsupported file format")
    {
    }
}