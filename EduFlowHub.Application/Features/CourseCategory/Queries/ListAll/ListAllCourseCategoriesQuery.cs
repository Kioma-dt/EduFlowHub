using EduFlowHub.Application.Features.CourseCategory.Dtos;
using EduFlowHub.Application.Features.Dtos;

namespace EduFlowHub.Application.Features.CourseCategory.Queries.ListAll;

public sealed record ListAllCourseCategoriesQuery(int Page = 1, int PageSize = 10)
    : IRequest<PagedDto<CourseCategoryDto>>;