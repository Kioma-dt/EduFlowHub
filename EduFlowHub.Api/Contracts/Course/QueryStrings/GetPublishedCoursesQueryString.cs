namespace EduFlowHub.Api.Contracts.Course.QueryStrings;

public sealed record GetPublishedCoursesQueryString(
    string? Category,
    Guid? AuthorId);