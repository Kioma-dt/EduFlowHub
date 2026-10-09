namespace EduFlowHub.Application.Abstractions.Services;

public interface ISupportedFileTypeChecker
{
    bool IsFileTypeSupported(string fileExtension);
}