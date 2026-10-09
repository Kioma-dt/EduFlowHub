using EduFlowHub.Application.Features.LessonMaterial.Dtos;
using EduFlowHub.Application.Features.LessonMaterial.Queries.ListAll;

namespace EduFlowHub.Application.Features.LessonMaterial.Queries.ListOfCourse;

public sealed record ListLessonMaterialsOfCourseQuery(
    Guid CourseId,
    bool OrderByNumber = false)
    : IRequest<IEnumerable<LessonMaterialDto>>;