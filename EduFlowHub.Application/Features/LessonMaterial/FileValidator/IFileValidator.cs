namespace EduFlowHub.Application.Features.LessonMaterial.FileValidator;

public interface IFileValidator
{
    void ValidateFile(Stream fileStream, string fileExtension, long fileLength);
}