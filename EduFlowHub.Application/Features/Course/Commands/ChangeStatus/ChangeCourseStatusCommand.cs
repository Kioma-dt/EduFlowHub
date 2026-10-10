using EduFlowHub.Application.Abstractions.Persistence.Repositories;
using EduFlowHub.Application.Features.Course.Dtos;

namespace EduFlowHub.Application.Features.Course.Commands.ChangeStatus;

public sealed record ChangeCourseStatusCommand(
    Guid CourseId,
    CourseStatus Status)
    : IRequest<CourseDto>;