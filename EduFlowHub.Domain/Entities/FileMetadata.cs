using EduFlowHub.Domain.Entities.Abstractions;

namespace EduFlowHub.Domain.Entities;

public class FileMetadata
    : Entity
{
    public Guid Id { get; set; }
    public string Path { get; set; } = null!;
    public string Extension { get; set; } = null!;
    public long Size { get; set; }
}