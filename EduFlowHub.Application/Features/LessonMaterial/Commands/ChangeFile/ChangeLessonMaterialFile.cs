using EduFlowHub.Application.Features.LessonMaterial.Dtos;

namespace EduFlowHub.Application.Features.LessonMaterial.Commands.ChangeFile;

public sealed record ChangeLessonMaterialFile(
    Guid LessonMaterialId,
    Stream FileStream)
    : IRequest<LessonMaterialDto>;