namespace EduFlowHub.Application.Features.LessonMaterial.Commands.ChangeFile;

public sealed record ChangeLessonMaterialFileCommand(
    Guid LessonMaterialId,
    Stream FileStream,
    string FileExtension,
    long FileLength)
    : IRequest;