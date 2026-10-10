namespace EduFlowHub.Application.Abstractions.Services.File;

public interface ISupportedFileTypeChecker
{
    bool IsFileTypeSupported(string fileExtension);
}