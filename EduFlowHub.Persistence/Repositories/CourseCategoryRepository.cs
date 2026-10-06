using EduFlowHub.Application.Abstractions.Persistence.Repositories;
using EduFlowHub.Persistence.Repositories.Generic;

namespace EduFlowHub.Persistence.Repositories;

public class CourseCategoryRepository
    : Repository<CourseCategory>, 
        ICourseCategoryRepository
        
{
    public CourseCategoryRepository(DbContext dbContext)
        : base(dbContext)
    {
    }
}