using EduFlowHub.Application.Features.CourseCategory.Dtos;
using EduFlowHub.Application.Features.User.Dtos;

namespace EduFlowHub.Application.Features.Course.Dtos;

public sealed record CourseDto(
    Guid Id,
    string Title,
    string Description,
    decimal Price,
    CourseStatus Status,
    UserDto Author,
    CourseCategoryDto Category);