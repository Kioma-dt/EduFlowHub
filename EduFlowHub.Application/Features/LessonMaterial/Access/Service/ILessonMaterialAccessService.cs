namespace EduFlowHub.Application.Features.LessonMaterial.Access.Service;

public interface ILessonMaterialAccessService
{
    void EnsureCanChangeFile(Domain.Entities.LessonMaterial lessonMaterial);
    void EnsureCanCreate(Guid authorId);
    void EnsureCanDelete(Domain.Entities.LessonMaterial lessonMaterial);
    void EnsureCanUpdate(Domain.Entities.LessonMaterial lessonMaterial);
    Task EnsureCanViewAsync(Guid courseId, CancellationToken cancellationToken = default);
}