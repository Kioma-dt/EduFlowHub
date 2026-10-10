namespace EduFlowHub.Application.Abstractions.Services;

public interface IFileStorageRequirements
{
    IReadOnlyList<string> Extensions { get; }
    int MaxFileSize { get; }
}