using EduFlowHub.Application.Features.CourseCategory.Commands.Create;
using EduFlowHub.Application.Features.CourseCategory.Dtos;

namespace EduFlowHub.Application.Features.CourseCategory.Commands.Delete;

public sealed record DeleteCourseCategoryCommand(Guid CourseCategoryId)
    : IRequest;