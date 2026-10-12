using EduFlowHub.Domain.Entities.Abstractions;

namespace EduFlowHub.Domain.Entities;

public class LessonMaterial
    : Entity
{
    private LessonMaterial()
    {
    }

    public LessonMaterial(
        string title,
        int orderNumber,
        MaterialType type,
        Guid courseId,
        FileMetadata fileMetadata)
    {
        Title = ValidateTitle(title);
        OrderNumber = ValidateOrderNumber(orderNumber);
        Type = type;
        CourseId = ValidateCourseId(courseId);
        FileMetadata = fileMetadata;
    }

    public void UpdateDetails(
        string title,
        int orderNumber)
    {
        Title = ValidateTitle(title);
        OrderNumber = ValidateOrderNumber(orderNumber);
    }

    public void ChangeCourse(Guid courseId)
    {
        CourseId = ValidateCourseId(courseId);
    }

    public void ChangeFile(
        FileMetadata fileMetadata,
        MaterialType type)
    {
        Type = type;
        FileMetadata = fileMetadata;
    }
    
    public string Title { get; private set; } = null!;
    public int OrderNumber { get; private set; }
    public MaterialType  Type { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    
    public Guid CourseId { get; private set; }
    
    public Course? Course { get; private set; }
    public FileMetadata? FileMetadata { get; private set; }
    
    private string ValidateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new DomainValidationException("Lesson Material title cannot be empty");
        }
        
        return title.Trim();
    }
    
    private int ValidateOrderNumber(int orderNumber)
    {
        if (orderNumber <= 0)
        {
            throw new DomainValidationException("Order number cannot be negative or zero");
        }

        return orderNumber;
    }

    private Guid ValidateCourseId(Guid courseId)
    {
        if (courseId == Guid.Empty)
        {
            throw new DomainValidationException("Course id cannot be empty");
        }

        return courseId;
    }
}