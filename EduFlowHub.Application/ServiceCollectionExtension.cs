using EduFlowHub.Application.Features;
using EduFlowHub.Application.Features.LessonMaterial.FileValidator;
using EduFlowHub.Application.Features.User.Options;
using EduFlowHub.Application.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EduFlowHub.Application;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var assembly = typeof(ServiceCollectionExtension).Assembly;

        services.AddAutoMapper(cfg => cfg.AddMaps(assembly));
        
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        services.AddAccess();

        services.AddFilters();
        
        services.AddScoped<IFileValidator, FileValidator>();
        services.AddOptions<UserLockoutOptions>()
            .Bind(configuration.GetSection(UserLockoutOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        return services;
    }
}