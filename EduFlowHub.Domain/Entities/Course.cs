using EduFlowHub.Domain.Entities.Abstractions;

namespace EduFlowHub.Domain.Entities;

public class Course
    : Entity
{
    private readonly List<LessonMaterial> _lessonMaterials = new();

    public Course()
    {
    }

    public Course(
        string title,
        string description,
        decimal price,
        Guid authorId,
        Guid categoryId
    )
    {
        Title = ValidateTitle(title);
        Description = ValidateDescription(description);
        Price = ValidatePrice(price);
        Status = CourseStatus.Draft;
        AuthorId = ValidateAuthorId(authorId);
        CategoryId = ValidateCategory(categoryId);
    }
    
    public string Title { get; private set; } = null!;

    public string Description { get; private set; } = null!;

    public decimal Price {get; private set;}

    public CourseStatus Status { get; private set; }
    
    public Guid AuthorId { get; private set; }
    public Guid CategoryId { get; private set; }
    
    public User? Author { get; private set; }
    public CourseCategory? Category { get; private set; }

    public IReadOnlyCollection<LessonMaterial> LessonMaterials => _lessonMaterials.AsReadOnly();

    public void UpdateDetails(
        string title,
        string description,
        decimal price)
    {
        Title = ValidateTitle(title);
        Description = ValidateDescription(description);
        Price = ValidatePrice(price);
    }

    public void ChangeAuthor(Guid authorId)
    {
        AuthorId = ValidateAuthorId(authorId);
    }

    public void ChangeCategory(Guid categoryId)
    {
        CategoryId = ValidateCategory(categoryId);
    }

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
    
    private string ValidateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new DomainValidationException("Course title cannot be empty");
        }
        
        return title.Trim();
    }
    
    private string ValidateDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new DomainValidationException("Course description cannot be empty");
        }
        
        return description.Trim();
    }

    private decimal ValidatePrice(decimal price)
    {
        if (price < 0)
        {
            throw new DomainValidationException("Course price cannot be negative");
        }

        return price;
    }

    private Guid ValidateAuthorId(Guid authorId)
    {
        if (authorId == Guid.Empty)
        {
            throw new DomainValidationException("Author id cannot be empty");
        }

        return authorId;
    }
    
    private Guid ValidateCategory(Guid categoryId)
    {
        if (categoryId == Guid.Empty)
        {
            throw new DomainValidationException("Category id cannot be empty");
        }

        return categoryId;
    }
}