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
}