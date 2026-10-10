using EduFlowHub.Application.Abstractions.Services;
using EduFlowHub.Infrastructure.FileServices.Abstractions;

namespace EduFlowHub.Infrastructure.FileServices.FileCheckers;

public class SupportedFileTypeChecker
    : ISupportedFileTypeChecker
{
    private readonly HashSet<string> _supportedExtensions;

    public SupportedFileTypeChecker(ISupportedFileTypes supportedFileTypes)
    {
        _supportedExtensions = new HashSet<string>(supportedFileTypes
            .Extensions
            .Select(extension => extension.ToLowerInvariant()));
    }

    public bool IsFileTypeSupported(string fileExtension)
    {
        return _supportedExtensions.Contains(fileExtension.ToLowerInvariant());
    }
}