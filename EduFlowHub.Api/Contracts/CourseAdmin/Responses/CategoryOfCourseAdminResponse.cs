namespace EduFlowHub.Api.Contracts.CourseAdmin.Responses;

public sealed record CategoryOfCourseAdminResponse(
    Guid Id,
    string Name,
    string NormalizedName
);