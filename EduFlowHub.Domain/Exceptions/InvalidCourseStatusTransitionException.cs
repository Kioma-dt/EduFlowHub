namespace EduFlowHub.Domain.Exceptions;

public class InvalidCourseStatusTransitionException
    : DomainWrongStatusException
{
    public InvalidCourseStatusTransitionException(
        CourseStatus current,
        CourseStatus target)
        : base($"Cannot change course status from '{current}' to '{target}'")
    {
    }
}