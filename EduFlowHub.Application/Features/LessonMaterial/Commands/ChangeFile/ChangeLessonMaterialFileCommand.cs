namespace EduFlowHub.Application.Features.LessonMaterial.Commands.ChangeFile;

public sealed record ChangeLessonMaterialFileCommand(
    Guid LessonMaterialId,
    Guid CourseId,
    Stream FileStream,
    string FileExtension,
    long FileLength)
    : IRequest;