namespace EduFlowHub.Domain.Entities;

public class Course
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public CourseStatus Status { get; set; }
    
    public Guid AuthorId { get; set; }
    public Guid CategoryId { get; set; }
    
    public User? Author { get; set; }
    public CourseCategory? Category { get; set; }

    public IReadOnlyCollection<LessonMaterial> LessonMaterials { get; set; } = new List<LessonMaterial>();
}