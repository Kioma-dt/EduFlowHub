using EduFlowHub.Application.Features.CourseCategory.Dtos;

namespace EduFlowHub.Application.Features.CourseCategory.Queries.ListAll;

public sealed record ListAllCourseCategoriesQuery()
    : IRequest<IEnumerable<CourseCategoryDto>>;