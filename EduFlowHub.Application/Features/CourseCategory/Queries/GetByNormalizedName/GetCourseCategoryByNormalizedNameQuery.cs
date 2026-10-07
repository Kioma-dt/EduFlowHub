using EduFlowHub.Application.Features.CourseCategory.Dtos;
using EduFlowHub.Application.Features.CourseCategory.Queries.GetById;

namespace EduFlowHub.Application.Features.CourseCategory.Queries.GetByNormalizedName;

public sealed record GetCourseCategoryByNormalizedNameQuery(
    string NormalizedName)
    : IRequest<CourseCategoryDto>;