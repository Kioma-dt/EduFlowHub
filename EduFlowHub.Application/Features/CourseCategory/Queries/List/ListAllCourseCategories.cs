using EduFlowHub.Application.Features.CourseCategory.Dtos;

namespace EduFlowHub.Application.Features.CourseCategory.Queries.List;

public sealed record ListAllCourseCategories()
    : IRequest<IEnumerable<CourseCategoryDto>>;