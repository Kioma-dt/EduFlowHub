using EduFlowHub.Application.Features.Course.Access.Policy;
using EduFlowHub.Application.Features.Course.Access.Service;
using EduFlowHub.Application.Features.CourseCategory.Access.Policy;
using EduFlowHub.Application.Features.CourseCategory.Access.Service;
using EduFlowHub.Application.Features.CourseEnrollment.Access.Policy;
using EduFlowHub.Application.Features.CourseEnrollment.Access.Service;
using EduFlowHub.Application.Features.LessonMaterial.Access.Policy;
using EduFlowHub.Application.Features.LessonMaterial.Access.Service;
using EduFlowHub.Application.Features.User.Access.Policy;
using EduFlowHub.Application.Features.User.Access.Service;
using Microsoft.Extensions.DependencyInjection;

namespace EduFlowHub.Application.Features;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddAccess(this IServiceCollection services)
    {
        services.AddScoped<ICourseAccessPolicy, CourseAccessPolicy>();
        services.AddScoped<ICourseAccessService, CourseAccessService>();
        
        services.AddScoped<ICourseCategoryAccessPolicy, CourseCategoryAccessPolicy>();
        services.AddScoped<ICourseCategoryAccessService, CourseCategoryAccessService>();
        
        services.AddScoped<ICourseEnrollmentAccessPolicy, CourseEnrollmentAccessPolicy>();
        services.AddScoped<ICourseEnrollmentAccessService, CourseEnrollmentAccessService>();
        
        services.AddScoped<ILessonMaterialAccessPolicy, LessonMaterialAccessPolicy>();
        services.AddScoped<ILessonMaterialAccessService, LessonMaterialAccessService>();
        
        services.AddScoped<IUserAccessPolicy, UserAccessPolicy>();
        services.AddScoped<IUserAccessService, UserAccessService>();
        
        return services;
    }
}