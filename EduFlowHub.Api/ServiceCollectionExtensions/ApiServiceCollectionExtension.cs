using EduFlowHub.Api.Services;
using EduFlowHub.Application.Abstractions.Services.User;

namespace EduFlowHub.Api.ServiceCollectionExtensions;

public static class ApiServiceCollectionExtension
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        var assembly = typeof(ApiServiceCollectionExtension).Assembly;

        services.AddAutoMapper(cfg => cfg.AddMaps(assembly));

        services.AddScoped<ICurrentUser, CurrentUser>();
        
        services.AddValidatorsFromAssembly(assembly);
        
        return services;
    }
}