namespace EduFlowHub.Application.Features.CourseEnrollment.Exceptions;

public class StudentIsNotEnrolledOnCourseException
    : ApplicationStateException
{
    public StudentIsNotEnrolledOnCourseException(Guid studentId,  Guid courseId) 
        : base($"Student: {studentId} is not enrolled on course: {courseId}")
    {
    }
}