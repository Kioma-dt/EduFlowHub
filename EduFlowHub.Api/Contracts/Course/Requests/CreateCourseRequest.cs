namespace EduFlowHub.Api.Contracts.Course.Requests;

public sealed record CreateCourseRequest(
    string Title,
    string Description,
    decimal Price,
    Guid CategoryId);