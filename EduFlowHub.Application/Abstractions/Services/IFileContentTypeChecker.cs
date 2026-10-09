namespace EduFlowHub.Application.Abstractions.Services;

public interface IFileContentTypeChecker
{
    bool IsContentTypeCorrect(Stream fileStream, string fileExtension);
}