namespace EduFlowHub.Infrastructure.FileServices.FileStorage.Local.Options;

public sealed class LocalSupportedFileExtensionsOptions
{
    public const string SectionName = "FileStorage:Local:SupportedFileFormats";

    public string[] Extensions { get; set; } = [];
}