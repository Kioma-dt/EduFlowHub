namespace EduFlowHub.Api.Contracts.Course.QueryStrings;

public sealed record GetStudentEnrolledCoursesQueryString(string? CategoryNormalizedName, Guid? AuthorId);