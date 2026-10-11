namespace EduFlowHub.Api.Contracts.Course.QueryStrings;

public sealed record GetAllCoursesQueryString(string? CategoryNormalizedName, CourseStatus? Status, Guid? AuthorId);