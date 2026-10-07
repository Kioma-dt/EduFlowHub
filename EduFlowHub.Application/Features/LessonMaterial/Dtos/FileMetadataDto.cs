namespace EduFlowHub.Application.Features.LessonMaterial.Dtos;

public sealed record FileMetadataDto(
    string Path,
    string Extension,
    long  Size);