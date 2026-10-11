namespace EduFlowHub.Api.Contracts.Course.Requests;

public sealed record CreateMyCourseRequest(
    string Title,
    string Description,
    decimal Price,
    Guid CategoryId);