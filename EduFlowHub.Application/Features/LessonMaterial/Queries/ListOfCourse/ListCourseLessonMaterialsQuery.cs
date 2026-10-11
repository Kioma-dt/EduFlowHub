using EduFlowHub.Application.Features.LessonMaterial.Dtos;

namespace EduFlowHub.Application.Features.LessonMaterial.Queries.ListOfCourse;

public sealed record ListLessonMaterialsOfCourseQuery(Guid CourseId, bool OrderByNumber = false) 
    : IRequest<IEnumerable<LessonMaterialDto>>;