namespace EduFlowHub.Api.Contracts.LessonMaterial.Requests;

public sealed record CreateLessonMaterialRequest(
    string Title,
    int OrderNumber,
    IFormFile File);
    
public sealed record UpdateLessonMaterialRequest(
    string? Title,
    int? OrderNumber);
    public sealed record ChangeLessonMaterialFileRequest(
    IFormFile File);