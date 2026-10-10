using EduFlowHub.Application.Features.CourseCategory.Commands.Create;
using EduFlowHub.Application.Features.CourseCategory.Dtos;

namespace EduFlowHub.Application.Features.CourseCategory.Commands.Update;

public sealed record UpdateCourseCategoryCommand(
    Guid CourseCategoryId,
    string Name)
    : IRequest<CourseCategoryDto>;