using EduFlowHub.Application.Features.Course.Dtos;

namespace EduFlowHub.Application.Features.Course.Queries.ListOfAuthor;

public sealed record ListCoursesOfAuthorQuery(
    Guid AuthorId,
    string? Category,
    CourseStatus? Status)
    : IRequest<IEnumerable<CourseDto>>;