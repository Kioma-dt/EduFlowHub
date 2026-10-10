namespace EduFlowHub.Api.Contracts.Course.Requests;

public sealed record UpdateCourseRequest(
    string? Title,
    string? Description,
    decimal? Price,
    Guid? CategoryId);