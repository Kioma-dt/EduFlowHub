using EduFlowHub.Application.Features.LessonMaterial.Dtos;

namespace EduFlowHub.Application.Features.LessonMaterial.Queries.ListCourse;

public sealed record ListCourseLessonMaterialsQuery(
    Guid CourseId,
    bool OrderByNumber = false)
    : IRequest<IEnumerable<LessonMaterialDto>>;