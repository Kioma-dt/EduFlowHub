namespace EduFlowHub.Domain.Exceptions;

public class InvalidCourseStatusTransitionException
    : Exception
{
    public InvalidCourseStatusTransitionException(
        CourseStatus current,
        CourseStatus target)
        : base($"Cannot change course status from '{current}' to '{target}'")
    {
    }
}