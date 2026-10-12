using EduFlowHub.Application.Features.Course.Dtos;
using EduFlowHub.Application.Features.Dtos;

namespace EduFlowHub.Application.Features.Course.Queries.ListPublished;

public sealed record ListPublishedCoursesQuery(
    Guid? AuthorId,
    string? CategoryNormalizedName,
    int Page = 1,
    int PageSize = 10)
    : IRequest<PagedDto<CourseDto>>;