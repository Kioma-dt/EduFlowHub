namespace EduFlowHub.Application.Features.LessonMaterial.Commands.Update;

public sealed record UpdateLessonMaterialCommand(Guid LessonMaterialId, Guid CourseId, string? Title, int? OrderNumber)
    : IRequest;