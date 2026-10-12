using EduFlowHub.Api.Seeders.AdminSeeder;

namespace EduFlowHub.Api.Seeders;

public static class SeedersServiceCollectionExtension
{
    public static IServiceCollection AddSeeders(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddOptions<InitialAdminOptions>()
            .Bind(configuration.GetSection(InitialAdminOptions.SectionName));
        services.AddHostedService<AdminSeeder.AdminSeeder>();
        
        if (configuration.GetSection("ASPNETCORE_ENVIRONMENT").Value == "Development")
        {
            services.AddHostedService<TestDataSeeder.TestDataSeeder>();
        }
        
        return services;
    }
}