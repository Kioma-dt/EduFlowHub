namespace EduFlowHub.Application.Features.CourseEnrollment.Commands.EnrollStudentOnCourse;

public sealed record EnrollStudentOnCourseCommand(
    Guid StudentId,
    Guid CourseId)
    : IRequest;