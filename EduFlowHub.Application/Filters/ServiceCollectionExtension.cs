using EduFlowHub.Application.Filters.Course;
using EduFlowHub.Application.Filters.Course.Specifications;
using EduFlowHub.Application.Filters.Course.Specifications.Abstraction;
using EduFlowHub.Application.Filters.Course.Specifications.Factory;
using Microsoft.Extensions.DependencyInjection;

namespace EduFlowHub.Application.Filters;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddFilters(this IServiceCollection services)
    {
        services.AddScoped<ICourseFilterSpecification, CourseFilterAuthorIdSpecification>();
        services.AddScoped<ICourseFilterSpecification, CourseFilterCategoryIdSpecification>();
        services.AddScoped<ICourseFilterSpecification, CourseFilterCategoryNormalizedNameSpecification>();
        services.AddScoped<ICourseFilterSpecification, CourseFilterStatusSpecification>();
        
        services.AddScoped<ICourseFilterSpecificationFactory, CourseFilterSpecificationFactory>();
        
        services.AddScoped<ICourseFilterBuilder, CourseFilterBuilder>();
        
        return services;
    } 
}