namespace EduFlowHub.Api.Contracts.Course.Responses;

public sealed record CategoryOfCourseResponse(
    Guid Id,
    string Name,
    string NormalizedName
);