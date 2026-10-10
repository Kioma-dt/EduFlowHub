namespace EduFlowHub.Application.Features.LessonMaterial.Dtos;

public sealed record FileMetadataDto(
    string Extension,
    long  Size);