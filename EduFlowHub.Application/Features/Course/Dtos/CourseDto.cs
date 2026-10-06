namespace EduFlowHub.Application.Features.Course.Dtos;

public sealed record CourseDto(
    Guid Id,
    string Title,
    string Description,
    decimal Price,
    CourseStatus Status,
    string AuthorName,
    string CategoryName);