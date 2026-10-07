using EduFlowHub.Application.Features.CourseEnrollment.Commands.EnrollStudentOnCourse;

namespace EduFlowHub.Application.Features.CourseEnrollment.Commands.UnenrollStudentFromCourse;

public sealed record UnenrollStudentFromCourseCommand(
    Guid StudentId,
    Guid CourseId)
    : IRequest;