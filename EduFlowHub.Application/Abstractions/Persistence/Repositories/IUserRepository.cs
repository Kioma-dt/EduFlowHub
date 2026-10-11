using System.Linq.Expressions;
using EduFlowHub.Application.Abstractions.Persistence.Repositories.Generic;
using EduFlowHub.Application.Features.User.Dtos;

namespace EduFlowHub.Application.Abstractions.Persistence.Repositories;

public interface IUserRepository
    : IRepository<User>
{
    Task<User?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<IEnumerable<User>> GetEnrolledOnCourse(
        Guid courseId, 
        Expression<Func<User, bool>>? filter = null,
        Func<IQueryable<User>, IOrderedQueryable<User>>? orderBy = null,
        int page = 1, 
        int pageSize = 10,
        CancellationToken cancellationToken = default);
}