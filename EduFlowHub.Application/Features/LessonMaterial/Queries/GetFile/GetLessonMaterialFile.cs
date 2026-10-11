using EduFlowHub.Application.Features.LessonMaterial.Dtos;

namespace EduFlowHub.Application.Features.LessonMaterial.Queries.GetFile;

public sealed record GetLessonMaterialFileQuery(Guid LessonMaterialId, Guid CourseId)
    : IRequest<FileDto>;