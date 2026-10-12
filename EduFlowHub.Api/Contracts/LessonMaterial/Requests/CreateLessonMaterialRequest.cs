namespace EduFlowHub.Api.Contracts.LessonMaterial.Requests;

public sealed record CreateLessonMaterialRequest(
    string Title,
    int OrderNumber,
    IFormFile File);