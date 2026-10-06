using EduFlowHub.Application.Abstractions.Persistence.Repositories;
using EduFlowHub.Persistence.Repositories.Generic;

namespace EduFlowHub.Persistence.Repositories;

public class CourseRepository
    : Repository<Course>, 
        ICourseRepository
        
{
    public CourseRepository(DbContext dbContext)
        : base(dbContext)
    {
    }
}