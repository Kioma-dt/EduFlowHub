namespace EduFlowHub.Application.Features.CourseEnrollment.Exceptions;

public class StudentAlreadyEnrolledOnCourseException
    : Exception
{
    public StudentAlreadyEnrolledOnCourseException(Guid studentId,  Guid courseId) 
        : base($"Student: {studentId} is already enrolled on course: {courseId}")
    {
    }
}