namespace EduFlowHub.Infrastructure.FileServices.Abstractions;

public interface ISupportedFileTypes
{
    IReadOnlyList<string> Extensions { get; }
}