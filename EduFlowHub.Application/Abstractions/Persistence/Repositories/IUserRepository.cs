using EduFlowHub.Application.Abstractions.Persistence.Repositories.Generic;
using EduFlowHub.Application.Features.User.Dtos;

namespace EduFlowHub.Application.Abstractions.Persistence.Repositories;

public interface IUserRepository
    : IRepository<User>
{
    Task<User?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<IEnumerable<User>> GetEnrolledOnCourse(Guid courseId, CancellationToken cancellationToken = default);
}