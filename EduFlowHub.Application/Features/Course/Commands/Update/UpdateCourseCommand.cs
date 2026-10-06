using EduFlowHub.Application.Features.Course.Dtos;

namespace EduFlowHub.Application.Features.Course.Commands.Update;

public sealed record UpdateCourseCommand(
    Guid CourseId,
    string? Title,
    string? Description,
    decimal? Price,
    CourseStatus? Status,
    Guid? AuthorId,
    Guid? CategoryId)
    : IRequest<CourseDto>;