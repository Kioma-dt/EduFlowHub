namespace EduFlowHub.Application.Exceptions.UnsupportedFileTypeExceptions;

public class UnsupportedFileTypeException
    : Exception
{
    public UnsupportedFileTypeException(string message)
        : base(message)
    {
    }
}