namespace EduFlowHub.Application.Features.Course.Commands.ChangeAuthor;

public sealed record ChangeCourseAuthorCommand(Guid CourseId, Guid AuthorId)
    : IRequest;