using EduFlowHub.Api.ServiceCollectionExtensions;
using EduFlowHub.Application;
using EduFlowHub.Infrastructure;
using EduFlowHub.Persistence;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace EduFlowHub.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        if (builder.Environment.IsDevelopment())
        {
            DotNetEnv.Env.TraversePath().Load(".env.development");
        }
        
        builder.Configuration.AddEnvironmentVariables();
        
        builder.WebHost.ConfigureKestrel(options =>
        {
            options.Limits.MaxRequestBodySize = 10 * 1024 * 1024;
            options.Limits.MinRequestBodyDataRate = new MinDataRate(240, TimeSpan.FromSeconds(10));
            options.Limits.RequestHeadersTimeout = TimeSpan.FromSeconds(30);
        });
        
        builder.Services.AddHttpContextAccessor();

        builder.Services
            .AddInfrastructure(builder.Configuration)
            .AddPersistence(builder.Configuration)
            .AddApplication(builder.Configuration)
            .AddApiServices();

        builder.Services
            .AddControllersConfigured()
            .AddProblemDetailsConfigured()
            .AddRateLimiterConfigured();
        
        builder.Services.AddAuthenticationConfigured(builder.Configuration)
            .AddAuthorization();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGenConfigured();
        
        var app = builder.Build();
        
        app.UseForwardedHeaders();      
        app.UseExceptionHandler();      
        app.UseStatusCodePages();
        
        //! if (app.Environment.IsDevelopment())
        //! {
            app.UseSwagger();
            app.UseSwaggerUI();
        //!}
        
        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthentication();
        app.UseRateLimiter();
        app.UseAuthorization();

        app.MapControllers();
        
        app.Run();
    }
}