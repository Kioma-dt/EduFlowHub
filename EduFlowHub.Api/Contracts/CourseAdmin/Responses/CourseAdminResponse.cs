namespace EduFlowHub.Api.Contracts.CourseAdmin.Responses;

public sealed record CourseAdminResponse(
    Guid Id,
    string Title,
    string Description,
    decimal Price,
    CourseStatus Status,
    AuthorOfCourseAdminResponse Author,
    CategoryOfCourseAdminResponse Category);