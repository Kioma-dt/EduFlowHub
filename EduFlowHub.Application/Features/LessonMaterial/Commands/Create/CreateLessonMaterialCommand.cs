using EduFlowHub.Application.Features.LessonMaterial.Dtos;

namespace EduFlowHub.Application.Features.LessonMaterial.Commands.Create;

public sealed record CreateLessonMaterialCommand(
    string Title,
    int OrderNumber,
    Stream FileStream,
    string FileExtension,
    long FileLength,
    Guid CourseId)
    : IRequest<LessonMaterialDto>;