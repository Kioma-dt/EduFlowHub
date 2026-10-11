using EduFlowHub.Application.Features.Course.Dtos;

namespace EduFlowHub.Application.Features.Course.Queries.ListPublished;

public sealed record ListPublishedCoursesQuery(
    Guid? AuthorId,
    string? CategoryNormalizedName)
    : IRequest<IEnumerable<CourseDto>>;