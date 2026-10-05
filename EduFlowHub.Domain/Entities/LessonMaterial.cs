using EduFlowHub.Domain.Entities.Abstractions;

namespace EduFlowHub.Domain.Entities;

public class LessonMaterial
    : Entity
{
    public string Title { get; set; } = null!;
    public int OrderNumber { get; set; }
    public MaterialType  Type { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public Guid CourseId { get; set; }
    public Guid FileMetadataId { get; set; }
    
    public Course? Course { get; set; }
    public FileMetadata? FileMetadata { get; set; }
}