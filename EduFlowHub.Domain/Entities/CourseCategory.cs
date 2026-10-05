namespace EduFlowHub.Domain.Entities;

public class CourseCategory
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string NormalizedName { get; set; } = null!;
    
    public IReadOnlyCollection<Course> Courses { get; set; } = new List<Course>();
}