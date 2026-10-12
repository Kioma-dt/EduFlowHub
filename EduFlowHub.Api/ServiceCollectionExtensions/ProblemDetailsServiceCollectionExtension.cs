using System.Diagnostics;

namespace EduFlowHub.Api.ServiceCollectionExtensions;

public static class ProblemDetailsServiceCollectionExtension
{
    public static IServiceCollection AddProblemDetailsConfigured(this IServiceCollection services)
    {
        services.AddProblemDetails(options =>
            options.CustomizeProblemDetails = context =>
            {
                var problemDetails = context.ProblemDetails;
                problemDetails.Instance ??= context.HttpContext.Request.Path;
                problemDetails.Extensions["traceId"] = Activity.Current?.Id ?? context.HttpContext.TraceIdentifier;
            });
        
        return services;
    }
}