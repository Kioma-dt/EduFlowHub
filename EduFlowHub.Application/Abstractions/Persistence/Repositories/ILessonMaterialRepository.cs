using EduFlowHub.Application.Abstractions.Persistence.Repositories.Generic;

namespace EduFlowHub.Application.Abstractions.Persistence.Repositories;

public interface ILessonMaterialRepository
    : IRepository<LessonMaterial>
{
    Task<LessonMaterial?> GetByCourseAndOrderNumberAsync(
        Guid courseId,
        int orderNumber);
}