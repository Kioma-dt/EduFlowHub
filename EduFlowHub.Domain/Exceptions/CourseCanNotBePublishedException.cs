namespace EduFlowHub.Domain.Exceptions;

public class CourseCanNotBePublishedException
    : DomainWrongStatusException{
    public CourseCanNotBePublishedException(string reason)
        : base($"Can not publish course: {reason}")
    {
    }
}