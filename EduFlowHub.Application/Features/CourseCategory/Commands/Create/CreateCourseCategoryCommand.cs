using EduFlowHub.Application.Features.Course.Commands.Create;
using EduFlowHub.Application.Features.Course.Dtos;
using EduFlowHub.Application.Features.CourseCategory.Dtos;

namespace EduFlowHub.Application.Features.CourseCategory.Commands.Create;

public sealed record CreateCourseCategoryCommand(string Name)
    : IRequest<CourseCategoryDto>;