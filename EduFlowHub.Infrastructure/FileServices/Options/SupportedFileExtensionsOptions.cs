namespace EduFlowHub.Infrastructure.FileSevices.Options;

public sealed class SupportedFileExtensionsOptions
{
    public const string SectionName = "FileStorage:SupportedFileFormats";

    public string[] Extensions { get; set; } = [];
}