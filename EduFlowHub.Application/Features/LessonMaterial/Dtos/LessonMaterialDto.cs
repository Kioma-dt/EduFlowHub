namespace EduFlowHub.Application.Features.LessonMaterial.Dtos;

public sealed record LessonMaterialDto(
    Guid Id,
    string Title,
    int OrderNumber,
    MaterialType MaterialType,
    DateTime CreatedAt,
    FileMetadataDto FileMetadata);