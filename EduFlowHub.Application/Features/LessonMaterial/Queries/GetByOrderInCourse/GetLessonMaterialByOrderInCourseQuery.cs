using EduFlowHub.Application.Features.LessonMaterial.Dtos;

namespace EduFlowHub.Application.Features.LessonMaterial.Queries.GetByOrderInCourse;

public sealed record GetLessonMaterialByOrderInCourseQuery(
    Guid CourseId,
    int OrderNumber)
    : IRequest<LessonMaterialDto>;