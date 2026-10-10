namespace EduFlowHub.Infrastructure.FileServices.FileStorage.Local.Options;

public class LocalMaxFileSizeOptions
{
    public const string SectionName = "FileStorage:Local";

    public int MaxFileSize { get; set; }
}
