using EduFlowHub.Application.Abstractions.Persistence.Repositories.Generic;
using EduFlowHub.Application.Features.Course.Dtos;
using EduFlowHub.Application.Features.Course.Queries.ListAll;

namespace EduFlowHub.Application.Abstractions.Persistence.Repositories;

public interface ICourseRepository
    : IRepository<Course>
{
    Task<IEnumerable<Course>> GetEnrolledCoursesAsync(Guid studentId);
    
    Task<IEnumerable<Course>> GetWithFiltersAsync(
        Guid? categoryId,
        Guid? authorId,
        CourseStatus? status);
}