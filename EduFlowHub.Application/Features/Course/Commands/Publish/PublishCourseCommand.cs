using EduFlowHub.Application.Features.Course.Dtos;

namespace EduFlowHub.Application.Features.Course.Commands.Publish;

public sealed record PublishCourseCommand(
    Guid CourseId)
    : IRequest<CourseDto>;