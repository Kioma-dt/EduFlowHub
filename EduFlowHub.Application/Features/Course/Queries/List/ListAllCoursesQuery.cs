using EduFlowHub.Application.Features.Course.Dtos;

namespace EduFlowHub.Application.Features.Course.Queries.List;

public sealed record ListAllCoursesQuery()
    : IRequest<IEnumerable<CourseDto>>;
