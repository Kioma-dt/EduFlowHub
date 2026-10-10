using EduFlowHub.Infrastructure.FileServices.Abstractions;
using EduFlowHub.Infrastructure.FileServices.FileStorage.Local.Options;
using Microsoft.Extensions.Options;

namespace EduFlowHub.Infrastructure.FileServices.FileStorage.Local.SupportedFileTypes;

public class LocalSupportedFileTypes
    : ISupportedFileTypes
{
    public IReadOnlyList<string> Extensions { get; }

    public LocalSupportedFileTypes(
        IOptions<LocalSupportedFileExtensionsOptions> options)
    {
        Extensions = options.Value.Extensions.ToList();
    }
}