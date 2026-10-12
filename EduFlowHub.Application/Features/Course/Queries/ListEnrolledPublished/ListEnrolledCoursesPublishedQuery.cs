using EduFlowHub.Application.Features.Course.Dtos;
using EduFlowHub.Application.Features.Dtos;

namespace EduFlowHub.Application.Features.Course.Queries.ListEnrolledPublished;

public record ListEnrolledCoursesPublishedQuery(
    Guid StudentId, 
    string? CategoryNormalizedName, 
    Guid? AuthorId,
    int Page = 1,
    int PageSize = 10)
    : IRequest<PagedDto<CourseDto>>;