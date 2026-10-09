using EduFlowHub.Application.Features.LessonMaterial.Dtos;
using EduFlowHub.Application.Features.LessonMaterial.Queries.GetByOrderInCourse;

namespace EduFlowHub.Application.Features.LessonMaterial.Queries.ListAll;

public sealed record ListAllLessonMaterialsQuery(bool OrderByNumber = false)
    : IRequest<IEnumerable<LessonMaterialDto>>;