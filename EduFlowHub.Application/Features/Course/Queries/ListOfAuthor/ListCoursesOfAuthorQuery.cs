using EduFlowHub.Application.Features.Course.Dtos;
using EduFlowHub.Application.Features.Dtos;

namespace EduFlowHub.Application.Features.Course.Queries.ListOfAuthor;

public sealed record ListCoursesOfAuthorQuery(
    Guid AuthorId,
    string? CategoryNormalizedName,
    CourseStatus? Status,
    int Page = 1,
    int PageSize = 10)
    : IRequest<PagedDto<CourseDto>>;