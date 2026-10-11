using System.Linq.Expressions;
using EduFlowHub.Application.Abstractions.Persistence.Repositories.Generic;
using EduFlowHub.Application.Features.Course.Dtos;
using EduFlowHub.Application.Features.Course.Queries.ListAll;

namespace EduFlowHub.Application.Abstractions.Persistence.Repositories;

public interface ICourseRepository
    : IRepository<Course>
{
    Task<Course?> GetByIdForAuthorAsync(Guid courseId, Guid authorId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Course>> GetEnrolledCoursesAsync(
        Guid studentId, 
        Expression<Func<Course, bool>> filter,
        CancellationToken cancellationToken = default);
}