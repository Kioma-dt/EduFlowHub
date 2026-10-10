namespace EduFlowHub.Api.Contracts.CourseAdmin.QueryStrings;

public sealed record GetAllCoursesQueryString(
    string? Category,
    CourseStatus? Status,
    Guid? AuthorId);