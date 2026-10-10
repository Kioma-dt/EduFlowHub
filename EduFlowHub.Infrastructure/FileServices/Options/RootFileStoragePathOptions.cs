namespace EduFlowHub.Infrastructure.FileServices.Options;

public class RootFileStoragePathOptions
{
    public const string SectionName = "FileStorage";

    public string RootPath { get; set; } = string.Empty;
}