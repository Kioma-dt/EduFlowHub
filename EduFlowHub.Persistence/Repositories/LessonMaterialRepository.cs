using EduFlowHub.Application.Abstractions.Persistence.Repositories;
using EduFlowHub.Persistence.Repositories.Generic;

namespace EduFlowHub.Persistence.Repositories;

public class LessonMaterialRepository
    : Repository<LessonMaterial>, 
        ILessonMaterialRepository
        
{
    public LessonMaterialRepository(DbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<LessonMaterial?> GetByCourseAndOrderNumberAsync(
        Guid courseId, 
        int orderNumber)
    {
        return await _dbSet
            .Include(lessonMaterial => lessonMaterial.Course)
            .Include(lessonMaterial => lessonMaterial.FileMetadata)
            .FirstOrDefaultAsync(lessonMaterial 
                => lessonMaterial.CourseId == courseId
                   && lessonMaterial.OrderNumber == orderNumber);
    }
}