using EduFlowHub.Application.Features;
using EduFlowHub.Application.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace EduFlowHub.Application;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(ServiceCollectionExtension).Assembly;

        services.AddAutoMapper(cfg => cfg.AddMaps(assembly));
        
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        services.AddAccess();

        services.AddFilters();
        
        return services;
    }
}