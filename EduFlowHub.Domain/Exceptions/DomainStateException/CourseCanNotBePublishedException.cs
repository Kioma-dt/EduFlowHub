namespace EduFlowHub.Domain.Exceptions.DomainStateException;

public class CourseCanNotBePublishedException
    : DomainStateException{
    public CourseCanNotBePublishedException(string reason)
        : base($"Can not publish course: {reason}")
    {
    }
}