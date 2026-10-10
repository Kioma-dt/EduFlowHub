namespace EduFlowHub.Application.Abstractions.Services.File;

public interface IFileStorageRequirements
{
    IReadOnlyList<string> Extensions { get; }
    int MaxFileSize { get; }
}