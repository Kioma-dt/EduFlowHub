using EduFlowHub.Application.Features.CourseCategory.Dtos;

namespace EduFlowHub.Application.Features.CourseCategory.Queries.GetById;

public sealed record GetCourseCategoryByIdQuery(
    Guid CourseCategoryId)
    : IRequest<CourseCategoryDto>;