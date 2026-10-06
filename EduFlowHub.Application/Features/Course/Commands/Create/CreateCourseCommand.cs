using EduFlowHub.Application.Features.Course.Dtos;
using EduFlowHub.Application.Features.User.Dtos;

namespace EduFlowHub.Application.Features.Course.Commands.Create;

public sealed record CreateCourseCommand(
    string Title,
    string Description,
    decimal Price,
    Guid AuthorId,
    Guid CategoryId)
    : IRequest<CourseDto>;