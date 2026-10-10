namespace EduFlowHub.Infrastructure.FileServices.FileStorage.Local.Options;

public class LocalRootFileStoragePathOptions
{
    public const string SectionName = "FileStorage:Local";

    public string RootPath { get; set; } = string.Empty;
}