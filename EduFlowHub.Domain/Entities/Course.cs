using EduFlowHub.Domain.Entities.Abstractions;

namespace EduFlowHub.Domain.Entities;

public class Course
    : Entity
{
    private string _title = null!;
    private string _description = null!;
    private decimal _price;

    private readonly List<LessonMaterial> _lessonMaterials = new();

    public string Title
    {
        get => _title;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new DomainValidationException("Course title cannot be empty");
            }
            _title = value;
        }
    }

    public string Description
    {
        get => _description;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new DomainValidationException("Course description cannot be empty");
            }
            _description = value;
        }
    }

    public decimal Price
    {
        get => _price;
        set
        {
            if (value < 0)
            {
                throw new DomainValidationException("Course price cannot be negative");
            }
            _price = value;
        }
    }
    
    public CourseStatus Status { get; set; }
    
    public Guid AuthorId { get; set; }
    public Guid CategoryId { get; set; }
    
    public User? Author { get; private set; }
    public CourseCategory? Category { get; private set; }

    public IReadOnlyCollection<LessonMaterial> LessonMaterials => _lessonMaterials;

    public void Archive()
    {
        if (Status != CourseStatus.Published)
        {
            throw new InvalidCourseStatusTransitionException(Status, CourseStatus.Archived);
        }
        
        Status = CourseStatus.Archived;
    }
    
    public void Publish()
    {
        if (Status != CourseStatus.Draft)
        {
            throw new InvalidCourseStatusTransitionException(Status, CourseStatus.Published);
        }
        
        Status = CourseStatus.Published;
    }
}