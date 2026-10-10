using EduFlowHub.Application.Abstractions.Persistence.Repositories;
using EduFlowHub.Persistence.Repositories.Generic;

namespace EduFlowHub.Persistence.Repositories;

public class UserRepository
    : Repository<User>, 
        IUserRepository
        
{
    public UserRepository(DbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<User?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(u => u.Name == name, cancellationToken);
    }
}