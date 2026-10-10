namespace EduFlowHub.Api.Contracts.LessonMaterial.Responses;

public sealed record LessonMaterialResponse(
    Guid Id,
    string Title,
    int OrderNumber,
    MaterialType MaterialType,
    DateTime CreatedAt,
    FileMetadataResponse FileMetadata);