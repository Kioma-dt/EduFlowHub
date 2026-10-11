namespace EduFlowHub.Api.Contracts.Course.QueryStrings;

public sealed record GetAllCoursesQueryString(string? Category, CourseStatus? Status, Guid? AuthorId);