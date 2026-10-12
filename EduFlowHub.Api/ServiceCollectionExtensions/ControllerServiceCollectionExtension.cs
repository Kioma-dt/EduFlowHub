namespace EduFlowHub.Api.ServiceCollectionExtensions;

public static class ControllerServiceCollectionExtension
{
    public static IServiceCollection AddControllersConfigured(this IServiceCollection services)
    {
        services.AddControllers(options => options.Filters.Add<ValidationFilter.ValidationFilter>());
        
        return services;
    }
}