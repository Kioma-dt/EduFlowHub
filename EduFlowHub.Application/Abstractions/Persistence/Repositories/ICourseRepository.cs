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
        Expression<Func<Course, bool>>? filter = null,
        Func<IQueryable<Course>, IOrderedQueryable<Course>>? orderBy = null,
        int page = 1, 
        int pageSize = 10,
        CancellationToken cancellationToken = default);
}