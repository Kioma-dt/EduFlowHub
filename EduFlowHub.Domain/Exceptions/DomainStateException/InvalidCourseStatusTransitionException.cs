namespace EduFlowHub.Domain.Exceptions.DomainStateException;

public class InvalidCourseStatusTransitionException
    : DomainStateException
{
    public InvalidCourseStatusTransitionException(
        CourseStatus current,
        CourseStatus target)
        : base($"Cannot change course status from '{current}' to '{target}'")
    {
    }
}