using EduFlowHub.Application.Exceptions.ApplicationStateExceptions;

namespace EduFlowHub.Application.Features.CourseEnrollment.Exceptions;

public class StudentAlreadyEnrolledOnCourseException
    : ApplicationStateException
{
    public StudentAlreadyEnrolledOnCourseException(Guid studentId,  Guid courseId) 
        : base($"Student: {studentId} is already enrolled on course: {courseId}")
    {
    }
}