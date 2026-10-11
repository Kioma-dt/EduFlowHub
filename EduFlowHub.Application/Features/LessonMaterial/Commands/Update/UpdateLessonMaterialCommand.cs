namespace EduFlowHub.Application.Features.LessonMaterial.Commands.Update;

public sealed record UpdateLessonMaterialCommand(Guid LessonMaterialId, string? Title, int? OrderNumber)
    : IRequest;