using EduFlowHub.Application.Abstractions.Services.File;
using EduFlowHub.Infrastructure.FileServices.FileStorage.Local.Options;
using Microsoft.Extensions.Options;

namespace EduFlowHub.Infrastructure.FileServices.FileStorage.Local.Requirments;

public class LocalFileStorageRequirments
    : IFileStorageRequirements
{
    public IReadOnlyList<string> Extensions { get; }
    
    public int MaxFileSize { get; }

    public LocalFileStorageRequirments(
        IOptions<LocalSupportedFileExtensionsOptions> supportedExtensionsOptions,
        IOptions<LocalMaxFileSizeOptions>  maxFileSizeOptions)
    {
        Extensions = supportedExtensionsOptions.Value.Extensions.ToList();
        MaxFileSize =  maxFileSizeOptions.Value.MaxFileSize;
    }
}