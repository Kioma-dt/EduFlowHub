using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Abstractions.Persistence.Repositories;
using EduFlowHub.Persistence.DbContexts;
using EduFlowHub.Persistence.Options;
using EduFlowHub.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EduFlowHub.Persistence;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<PostgresConnectionStringOptions>()
            .Bind(configuration.GetSection(PostgresConnectionStringOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        var connectionString = configuration
            .GetSection(PostgresConnectionStringOptions.SectionName)
            .Get<PostgresConnectionStringOptions>() 
                               ?? throw new InvalidOperationException("Connection string section is missing");;

        var host = connectionString.DB_HOST;
        var port = connectionString.DB_PORT;
        var db = connectionString.DB_NAME;
        var user = connectionString.DB_USER;
        var password = connectionString.DB_PASSWORD;
        services.AddDbContext<EducationDbContext>(options =>
        {
            options.UseNpgsql($"Host={host};Port={port};Database={db};Username={user};Password={password}");
        });

        services.AddScoped<ICourseCategoryRepository, CourseCategoryRepository>();
        services.AddScoped<ICourseEnrollmentRepository, CourseEnrollmentRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<IFileMetadataRepository, FileMetadataRepository>();
        services.AddScoped<ILessonMaterialRepository, LessonMaterialRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }
}