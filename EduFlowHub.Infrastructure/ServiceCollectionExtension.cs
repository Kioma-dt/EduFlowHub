using EduFlowHub.Application.Abstractions.Services.Authentication;
using EduFlowHub.Application.Abstractions.Services.File;
using EduFlowHub.Infrastructure.AuthenticationServices.JwtTokenGenerator;
using EduFlowHub.Infrastructure.AuthenticationServices.JwtTokenGenerator.Options;
using EduFlowHub.Infrastructure.AuthenticationServices.PasswordHasher;
using EduFlowHub.Infrastructure.FileServices.FileCheckers;
using EduFlowHub.Infrastructure.FileServices.FileStorage.Local;
using EduFlowHub.Infrastructure.FileServices.FileStorage.Local.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EduFlowHub.Infrastructure;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<JwtAuthenticationOptions>()
            .Bind(configuration.GetSection(JwtAuthenticationOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        services.AddOptions<LocalMaxFileSizeOptions>()
            .Bind(configuration.GetSection(LocalMaxFileSizeOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        services.AddOptions<LocalRootFileStoragePathOptions>()
            .Bind(configuration.GetSection(LocalRootFileStoragePathOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        services.AddOptions<LocalSupportedFileExtensionsOptions>()
            .Bind(configuration.GetSection(LocalSupportedFileExtensionsOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();

        services.AddScoped<IFileContentTypeChecker, MimeDetectiveFileContentTypeChecker>();
        services.AddScoped<ISupportedFileTypeChecker, SupportedFileTypeChecker>();

        services.AddScoped<IFileStorage, LocalFileStorage>();
        
        return services;
    }
}