namespace EduFlowHub.Api.Contracts.CourseAdmin.Requests;

public sealed record ChangeCourseStatusRequest(
    CourseStatus Status
);