namespace EduFlowHub.Application.Abstractions.Services.File;

public interface IFileContentTypeChecker
{
    bool IsContentTypeCorrect(Stream fileStream, string fileExtension);
}