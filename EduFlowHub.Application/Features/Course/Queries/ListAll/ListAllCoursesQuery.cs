using EduFlowHub.Application.Features.Course.Dtos;
using EduFlowHub.Application.Features.Dtos;

namespace EduFlowHub.Application.Features.Course.Queries.ListAll;

public sealed record ListAllCoursesQuery(
    string? CategoryNormalizedName,
    Guid? AuthorId,
    CourseStatus? Status,
    int Page = 1,
    int PageSize = 10)
    : IRequest<PagedDto<CourseDto>>;