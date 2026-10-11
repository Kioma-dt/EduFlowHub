namespace EduFlowHub.Application.Features.Course.Commands.Delete;

public sealed record DeleteCourseCommand(Guid CourseId)
    : IRequest;