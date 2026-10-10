using System.Text.RegularExpressions;
using EduFlowHub.Domain.Entities.Abstractions;

namespace EduFlowHub.Domain.Entities;

public class CourseCategory
    : Entity
{
    private readonly List<Course> _courses = new();

    private CourseCategory()
    {
    }
    
    public CourseCategory(string name)
    {
        Name = ValidateName(name);
        NormalizedName = NormalizeName(Name);
    }
    
    //! КОСТЫЛЬ
    public void SetId(Guid id)
    {
        Id = id;
    }
    
    public string Name { get; private set; } = null!;
    public string NormalizedName { get; private set; } = null!;
    
    public IReadOnlyCollection<Course> Courses  => _courses.AsReadOnly();

    public void UpdateDetails(string name)
    {
        Name = ValidateName(name);
        NormalizedName = NormalizeName(Name);
    }
    
    private string ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainValidationException("Course description cannot be empty");
        }
        
        return name.Trim();
    }
    
    private string NormalizeName(string name)
    {
        return Regex.Replace(
            name.Trim().ToLowerInvariant(),
            @"\s+",
            "-");
    }
}