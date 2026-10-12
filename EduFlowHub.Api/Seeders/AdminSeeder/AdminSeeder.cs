using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Abstractions.Services.Authentication;
using Microsoft.Extensions.Options;

namespace EduFlowHub.Api.Seeders.AdminSeeder;

public class AdminSeeder 
    : IHostedService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly InitialAdminOptions _initialAdminOptions;

    public AdminSeeder(
        IServiceScopeFactory scopes,
        IOptions<InitialAdminOptions> initialAdminOptions)
    {
        _scopeFactory = scopes;
        _initialAdminOptions = initialAdminOptions.Value;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(_initialAdminOptions.Username) 
            || string.IsNullOrWhiteSpace(_initialAdminOptions.Password))
            return;

        await using var scope = _scopeFactory.CreateAsyncScope();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();

        if (await unitOfWork.UserRepository.AnyByRoleAsync(UserRole.Admin, cancellationToken))
        {
            return;   
        }

        var user = new User(
            _initialAdminOptions.Username, passwordHasher.HashPassword(_initialAdminOptions.Password), UserRole.Admin,
            _initialAdminOptions.FirstName, _initialAdminOptions.LastName);
        
            await unitOfWork.UserRepository.InsertAsync(user, cancellationToken);
            await unitOfWork.SaveAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}