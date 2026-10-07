using EduFlowHub.Application.Features.LessonMaterial.Dtos;

namespace EduFlowHub.Application.Features.LessonMaterial.Queries.ListAll;

public sealed record ListAllLessonMaterialsQuery()
    : IRequest<IEnumerable<LessonMaterialDto>>;