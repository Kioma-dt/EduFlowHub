namespace EduFlowHub.Api.Contracts.LessonMaterial.Requests;

public sealed record ChangeLessonMaterialFileRequest(
    IFormFile File);