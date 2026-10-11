using EduFlowHub.Application.Features.LessonMaterial.Dtos;

namespace EduFlowHub.Application.Features.LessonMaterial.Queries.GetById;

public sealed record GetLessonMaterialByIdQuery(Guid LessonMaterialId)
    : IRequest<LessonMaterialDto>;