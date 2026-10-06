using EduFlowHub.Application.Features.CourseCategory.Dtos;

namespace EduFlowHub.Application.Features.CourseCategory.Queries.Get;

public sealed record GetCourseCategoryByNormalizedNameQuery(
    string NormalizedName)
    : IRequest<CourseCategoryDto>;