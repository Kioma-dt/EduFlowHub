namespace EduFlowHub.Api.Contracts.Course.QueryStrings;

public sealed record GetMyCoursesQueryString(
    string? Category,
    CourseStatus? Status);