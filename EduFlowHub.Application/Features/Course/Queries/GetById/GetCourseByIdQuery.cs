using EduFlowHub.Application.Features.Course.Dtos;

namespace EduFlowHub.Application.Features.Course.Queries.GetById;

public sealed record GetCourseByIdQuery(Guid CourseId)
    : IRequest<CourseDto>;