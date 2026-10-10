using EduFlowHub.Application.Abstractions.Persistence.Repositories.Generic;

namespace EduFlowHub.Application.Abstractions.Persistence.Repositories;

public interface IUserRepository
    : IRepository<User>
{
    Task<User?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}