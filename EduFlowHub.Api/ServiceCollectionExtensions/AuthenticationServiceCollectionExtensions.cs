using System.Security.Claims;
using System.Text;
using EduFlowHub.Infrastructure.AuthenticationServices.JwtTokenGenerator.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace EduFlowHub.Api.ServiceCollectionExtensions;

public static class AuthenticationServiceCollectionExtensions
{
    public static IServiceCollection AddAuthenticationConfigured(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var authOptions = configuration.GetSection(JwtAuthenticationOptions.SectionName)
            .Get<JwtAuthenticationOptions>();

        if (authOptions is null)
        {
            throw new InvalidOperationException(
                $"Missing {JwtAuthenticationOptions.SectionName} configuration section");
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions.SecretKey));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = authOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = authOptions.Audience,
                    ValidateLifetime = true,
                    IssuerSigningKey = key,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.FromSeconds(30),
                    ValidAlgorithms = [SecurityAlgorithms.HmacSha256],
                    NameClaimType = ClaimTypes.NameIdentifier,
                    RoleClaimType = ClaimTypes.Role
                };
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userId = context.Principal?
                            .FindFirst(ClaimTypes.NameIdentifier)
                            ?.Value;

                        if (string.IsNullOrEmpty(userId))
                        {
                            context.Fail("JWT token does not contain user id");
                        }

                        return Task.CompletedTask;
                    },
                    OnChallenge = async context =>
                    {
                        context.HandleResponse();

                        var problems = context.HttpContext.RequestServices.GetRequiredService<IProblemDetailsService>();
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.Headers.WWWAuthenticate = "Bearer";

                        await problems.TryWriteAsync(new ProblemDetailsContext
                        {
                            HttpContext = context.HttpContext,
                            ProblemDetails =
                            {
                                Status = 401, Title = "Unauthorized",
                                Detail = "Authentication is required"
                            }
                        });
                    },

                    OnForbidden = async context =>
                    {
                        var problems = context.HttpContext.RequestServices.GetRequiredService<IProblemDetailsService>();
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;

                        await problems.TryWriteAsync(new ProblemDetailsContext
                        {
                            HttpContext = context.HttpContext,
                            ProblemDetails =
                            {
                                Status = 403, Title = "Forbidden",
                                Detail = "You do not have permission to perform this action"
                            }
                        });
                    }
                };

            });
        return services;
    }
}