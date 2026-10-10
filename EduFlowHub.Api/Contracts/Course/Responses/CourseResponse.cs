namespace EduFlowHub.Api.Contracts.Course.Responses;

public sealed record CourseResponse(
    Guid Id,
    string Title,
    string Description,
    decimal Price,
    CourseStatus Status,
    AuthorOfCourseResponse Author,
    CategoryOfCourseResponse Category);