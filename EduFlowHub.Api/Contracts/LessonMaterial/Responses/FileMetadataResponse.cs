namespace EduFlowHub.Api.Contracts.LessonMaterial.Responses;

public sealed record FileMetadataResponse(
    string Extension,
    long  Size
);