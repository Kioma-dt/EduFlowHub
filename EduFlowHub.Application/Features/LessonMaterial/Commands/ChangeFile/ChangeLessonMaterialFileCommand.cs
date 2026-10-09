using EduFlowHub.Application.Features.LessonMaterial.Dtos;

namespace EduFlowHub.Application.Features.LessonMaterial.Commands.ChangeFile;

public sealed record ChangeLessonMaterialFileCommand(
    Guid LessonMaterialId,
    Stream FileStream,
    string FileExtension)
    : IRequest<LessonMaterialDto>;