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

    public async Task<CourseCategory?> GetByNormalizedNameAsync(string normalizedName,  CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(
            category => category.NormalizedName == normalizedName, 
            cancellationToken);
    }
}