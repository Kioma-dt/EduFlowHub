using EduFlowHub.Application.Features.Course.Dtos;

namespace EduFlowHub.Application.Features.Course.Queries.ListOfAuthor;

public sealed record ListCoursesOfAuthorQuery(
    Guid AuthorId,
    string? CategoryNormalizedName,
    CourseStatus? Status)
    : IRequest<IEnumerable<CourseDto>>;