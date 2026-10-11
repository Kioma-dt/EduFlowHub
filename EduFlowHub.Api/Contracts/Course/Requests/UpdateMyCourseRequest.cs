namespace EduFlowHub.Api.Contracts.Course.Requests;

public sealed record UpdateMyCourseRequest(
    string? Title,
    string? Description,
    decimal? Price,
    Guid? CategoryId);