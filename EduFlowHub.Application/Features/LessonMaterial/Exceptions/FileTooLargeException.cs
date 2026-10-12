using EduFlowHub.Application.Exceptions.ContentTooLarge;

namespace EduFlowHub.Application.Features.LessonMaterial.Exceptions;

public class FileTooLargeException
    : ContentTooLargeException
{
    public FileTooLargeException(int maxFileSize)
        : base($"File too large. Max Size: {maxFileSize}")
    {
    }
}