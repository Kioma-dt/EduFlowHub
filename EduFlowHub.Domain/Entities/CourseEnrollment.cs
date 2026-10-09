namespace EduFlowHub.Domain.Entities;

public class CourseEnrollment
{
    private CourseEnrollment()
    {
    }

    public CourseEnrollment(
        Guid courseId,
        Guid studentId)
    {
        CourseId = ValidateCourseId(courseId);
        StudentId = ValidateStudentId(studentId);
    }
    
    public Guid CourseId { get; private set; }
    public Guid StudentId { get; private set; }
    
    public Course? Course { get; private set; }
    public User? Student { get; private set; }
    
    private Guid ValidateCourseId(Guid courseId)
    {
        if (courseId == Guid.Empty)
        {
            throw new DomainValidationException("Course id cannot be empty");
        }

        return courseId;
    }
    
    private Guid ValidateStudentId(Guid studentId)
    {
        if (studentId == Guid.Empty)
        {
            throw new DomainValidationException("Student id cannot be empty");
        }

        return studentId;
    }
}