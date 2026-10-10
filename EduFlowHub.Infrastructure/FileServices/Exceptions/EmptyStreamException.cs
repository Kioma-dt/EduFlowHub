namespace EduFlowHub.Infrastructure.FileServices.Exceptions;

public class EmptyStreamException
    : FileStorageException
{
    public EmptyStreamException()
        : base("Stream is empty")
    {
    }
}