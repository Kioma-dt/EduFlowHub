using EduFlowHub.Application.Features.CourseCategory.Commands.Create;
using EduFlowHub.Application.Features.CourseCategory.Dtos;

namespace EduFlowHub.Application.Features.CourseCategory.Commands.UpdateOrCreate;

public sealed record UpdateOrCreateCourseCategoryCommand(
    Guid CourseCategoryId,
    string Name)
    : IRequest<UpdatedOrCreatedCourseCategoryDto>;