namespace EduFlowHub.Domain.Entities;

public class LessonMaterial
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public int OrderNumber { get; set; }
    public MaterialType  Type { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public Guid CourseId { get; set; }
    public Guid FileInfoId { get; set; }
}