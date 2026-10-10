namespace EduFlowHub.Infrastructure.FileServices.Options;

public class MaxFileSizeOptions
{
    public const string SectionName = "FileStorage";

    public int MaxFileSize { get; set; }
}