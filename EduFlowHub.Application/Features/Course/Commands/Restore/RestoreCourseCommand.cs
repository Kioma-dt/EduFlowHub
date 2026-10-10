namespace EduFlowHub.Application.Features.Course.Commands.Restore;

public sealed record RestoreCourseCommand(Guid CourseId)
    : IRequest;