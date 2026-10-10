namespace EduFlowHub.Application.Features.LessonMaterial.Dtos;

public sealed record FileDto(
    Stream FileStream,
    string FileName,
    string FileExtension);