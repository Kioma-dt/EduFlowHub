using EduFlowHub.Application.Abstractions.Services;

namespace EduFlowHub.Infrastructure.FileServices.FileCheckers;

public class SupportedFileTypeChecker
    : ISupportedFileTypeChecker
{
    private readonly HashSet<string> _supportedExtensions;

    public SupportedFileTypeChecker(IFileStorageRequirements fileStorageRequirements)
    {
        _supportedExtensions = new HashSet<string>(fileStorageRequirements
            .Extensions
            .Select(extension => extension.ToLowerInvariant()));
    }

    public bool IsFileTypeSupported(string fileExtension)
    {
        return _supportedExtensions.Contains(fileExtension.ToLowerInvariant());
    }
}