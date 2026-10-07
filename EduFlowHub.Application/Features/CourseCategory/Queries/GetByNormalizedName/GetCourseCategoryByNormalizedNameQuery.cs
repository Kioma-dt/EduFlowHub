using EduFlowHub.Application.Features.CourseCategory.Dtos;

namespace EduFlowHub.Application.Features.CourseCategory.Queries.GetByNormalizedName;

public sealed record GetCourseCategoryByNormalizedNameQuery(
    string NormalizedName)
    : IRequest<CourseCategoryDto>;