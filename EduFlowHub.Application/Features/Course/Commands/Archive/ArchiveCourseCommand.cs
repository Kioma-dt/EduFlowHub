namespace EduFlowHub.Application.Features.Course.Commands.Archive;

public sealed record ArchiveCourseCommand(Guid CourseId)
    : IRequest;