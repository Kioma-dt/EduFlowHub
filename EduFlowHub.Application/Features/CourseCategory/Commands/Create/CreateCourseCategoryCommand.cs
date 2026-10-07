using EduFlowHub.Application.Features.CourseCategory.Dtos;

namespace EduFlowHub.Application.Features.CourseCategory.Commands.Create;

public sealed record CreateCourseCategoryCommand(
    string Name,
    string NormalizedName)
    : IRequest<CourseCategoryDto>;