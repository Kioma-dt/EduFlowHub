using EduFlowHub.Application.Exceptions;

namespace EduFlowHub.Application.Features.LessonMaterial.Exceptions;

public class UnsupportedFileExtensionException
    : ApplicationValidationException
{
    public UnsupportedFileExtensionException(string extension)
        : base($"File extension ${extension} is not supported")
    {
    }
}