namespace EduFlowHub.Application.Features.Course.Commands.Unpublish;

public sealed record UnpublishCourseCommand(Guid CourseId)
    : IRequest;