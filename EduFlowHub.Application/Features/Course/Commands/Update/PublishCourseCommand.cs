using EduFlowHub.Application.Features.Course.Dtos;

namespace EduFlowHub.Application.Features.Course.Commands.Update;

public sealed record PublishCourseCommand(
    Guid CourseId)
    : IRequest<CourseDto>;