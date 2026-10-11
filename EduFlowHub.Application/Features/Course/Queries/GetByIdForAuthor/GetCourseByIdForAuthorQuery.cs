using EduFlowHub.Application.Features.Course.Dtos;

namespace EduFlowHub.Application.Features.Course.Queries.GetByIdForAuthor;

public sealed record GetCourseByIdForAuthorQuery(Guid CourseId, Guid AuthorId)
    : IRequest<CourseDto>;