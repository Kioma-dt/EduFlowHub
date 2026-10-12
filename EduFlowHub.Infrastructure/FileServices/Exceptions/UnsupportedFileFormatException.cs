namespace EduFlowHub.Infrastructure.FileServices.Exceptions;

public class UnsupportedFileFormatStorageException
    : FileStorageException
{
    public  UnsupportedFileFormatStorageException(string extension)
        : base($"Unsupported file format with extension {extension}")
    {
    }
}