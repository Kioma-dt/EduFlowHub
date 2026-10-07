using EduFlowHub.Application.Features.Course.Commands.Create;
using EduFlowHub.Application.Features.Course.Dtos;

namespace EduFlowHub.Application.Features.CourseEnrollment.Commands.EnrollStudentOnCourse;

public sealed record EnrollStudentOnCourseCommand(
    Guid StudentId,
    Guid CourseId)
    : IRequest;