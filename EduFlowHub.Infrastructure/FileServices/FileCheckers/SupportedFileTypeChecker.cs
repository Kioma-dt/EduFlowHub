using EduFlowHub.Application.Abstractions.Services;
using EduFlowHub.Infrastructure.FileSevices.Options;
using Microsoft.Extensions.Options;

namespace EduFlowHub.Infrastructure.FileServices.FileCheckers;

public class SupportedFileTypeChecker
    : ISupportedFileTypeChecker
{
    private readonly HashSet<string> _supportedExtensions;

    public SupportedFileTypeChecker(
        IOptions<SupportedFileExtensionsOptions>  options)
    {
        _supportedExtensions = new HashSet<string>(options
            .Value
            .Extensions
            .Select(extension => extension.ToLowerInvariant()));
    }

    public bool IsFileTypeSupported(string fileExtension)
    {
        return _supportedExtensions.Contains(fileExtension.ToLowerInvariant());
    }
}