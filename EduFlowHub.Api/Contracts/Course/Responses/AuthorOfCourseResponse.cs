namespace EduFlowHub.Api.Contracts.Course.Responses;

public sealed record AuthorOfCourseResponse(
    Guid Id,
    string FirstName,
    string LastName
);