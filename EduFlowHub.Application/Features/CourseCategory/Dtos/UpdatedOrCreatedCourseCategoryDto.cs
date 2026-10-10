namespace EduFlowHub.Application.Features.CourseCategory.Dtos;

public sealed record UpdatedOrCreatedCourseCategoryDto(
    CourseCategoryDto CourseCategory,
    bool Created);