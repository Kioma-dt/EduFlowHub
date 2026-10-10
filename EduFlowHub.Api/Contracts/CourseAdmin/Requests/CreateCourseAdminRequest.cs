namespace EduFlowHub.Api.Contracts.CourseAdmin.Requests;

public sealed record CreateCourseAdminRequest(
    string Title,
    string Description,
    decimal Price,
    Guid AuthorId,
    Guid CategoryId);