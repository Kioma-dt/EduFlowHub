using EduFlowHub.Application.Features.Course.Dtos;

namespace EduFlowHub.Application.Features.Course.Queries.ListAll;

public sealed record ListAllCoursesQuery(
    string? CategoryNormalizedName,
    Guid? AuthorId,
    CourseStatus? Status)
    : IRequest<IEnumerable<CourseDto>>;