using EduFlowHub.Application.Features.Course.Dtos;

namespace EduFlowHub.Application.Features.Course.Queries.ListEnrolled;

public record ListEnrolledCoursesQuery(Guid StudentId)
    : IRequest<IEnumerable<CourseDto>>;