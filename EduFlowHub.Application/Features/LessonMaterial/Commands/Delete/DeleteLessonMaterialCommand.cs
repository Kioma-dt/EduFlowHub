namespace EduFlowHub.Application.Features.LessonMaterial.Commands.Delete;

public sealed record DeleteLessonMaterialCommand(Guid LessonMaterialId, Guid CourseId)
    : IRequest;