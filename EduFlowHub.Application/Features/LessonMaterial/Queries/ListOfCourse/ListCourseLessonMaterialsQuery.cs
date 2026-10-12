using EduFlowHub.Application.Features.Dtos;
using EduFlowHub.Application.Features.LessonMaterial.Dtos;

namespace EduFlowHub.Application.Features.LessonMaterial.Queries.ListOfCourse;

public sealed record ListLessonMaterialsOfCourseQuery(
    Guid CourseId, 
    bool OrderByNumber = false,
    int  Page = 1, 
    int PageSize = 10)
    : IRequest<PagedDto<LessonMaterialDto>>;