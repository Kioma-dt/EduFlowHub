namespace EduFlowHub.Application.Features.LessonMaterial.Exceptions;

public class UnsupportedFileTypeException
    : ApplicationValidationException
{
    public UnsupportedFileTypeException(string extension)
        : base($"File extension ${extension} is not supported")
    {
    }
}