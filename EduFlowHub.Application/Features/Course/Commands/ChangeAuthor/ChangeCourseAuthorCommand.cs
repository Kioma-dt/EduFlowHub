using EduFlowHub.Application.Features.Course.Dtos;
using EduFlowHub.Application.Features.LessonMaterial.Dtos;

namespace EduFlowHub.Application.Features.Course.Commands.ChangeAuthor;

public sealed record ChangeCourseAuthorCommand(Guid CourseId, Guid AuthorId)
    : IRequest;