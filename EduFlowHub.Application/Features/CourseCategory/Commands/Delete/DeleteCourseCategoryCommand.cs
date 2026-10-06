namespace EduFlowHub.Application.Features.CourseCategory.Commands.Delete;

public sealed record DeleteCourseCategoryCommand(
    Guid CourseCategoryId)
    : IRequest;