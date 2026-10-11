using EduFlowHub.Application.Features.Course.Dtos;

namespace EduFlowHub.Application.Features.Course.Commands.Create;

public sealed record CreateCourseCommand(
    string Title,
    string Description,
    decimal Price,
    Guid CategoryId,
    Guid AuthorId)
    : IRequest<CourseDto>;