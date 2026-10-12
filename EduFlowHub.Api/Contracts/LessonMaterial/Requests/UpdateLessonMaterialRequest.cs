namespace EduFlowHub.Api.Contracts.LessonMaterial.Requests;

public sealed record UpdateLessonMaterialRequest(
    string? Title,
    int? OrderNumber);