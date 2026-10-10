namespace EduFlowHub.Api.Contracts.CourseAdmin.Responses;

public sealed record AuthorOfCourseAdminResponse(
    Guid Id,
    string FirstName,
    string LastName
);