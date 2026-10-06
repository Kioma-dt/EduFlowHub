namespace EduFlowHub.Application.Features.CourseCategory.Dtos;

public sealed record CourseCategoryDto(
    Guid Id,
    string Name,
    string NormalizedName);