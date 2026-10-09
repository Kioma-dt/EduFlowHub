namespace EduFlowHub.Application.Features.LessonMaterial.Exceptions;

public class FileContentTypeMismatchException
    : ApplicationValidationException
{
    public FileContentTypeMismatchException()
        : base("The content of file '{fileName}' does not match its file type")
    {
    }
}