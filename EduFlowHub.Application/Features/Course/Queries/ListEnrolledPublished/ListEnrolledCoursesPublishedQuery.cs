using EduFlowHub.Application.Features.Course.Dtos;

namespace EduFlowHub.Application.Features.Course.Queries.ListEnrolledPublished;

public record ListEnrolledCoursesPublishedQuery(
    Guid StudentId, 
    string? CategoryNormalizedName, 
    Guid? AuthorId)
    : IRequest<IEnumerable<CourseDto>>;