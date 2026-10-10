namespace EduFlowHub.Api.Contracts.CourseCategory.Responses;

public sealed record CourseCategoryResponse(
    Guid Id,
    string Name,
    string NormalizedName);