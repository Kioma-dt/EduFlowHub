using EduFlowHub.Api.Contracts.CourseCategory.Requests;
using EduFlowHub.Api.Contracts.CourseCategory.Responses;
using EduFlowHub.Application.Features.CourseCategory.Commands.Create;
using EduFlowHub.Application.Features.CourseCategory.Dtos;

namespace EduFlowHub.Api.Contracts.CourseCategory.Mapping;

public class CourseCategoryMappingProfile
    :  Profile
{
    public CourseCategoryMappingProfile()
    {
        CreateMap<CreateCourseCategoryRequest, CreateCourseCategoryCommand>();
        
        CreateMap<CourseCategoryDto, CourseCategoryResponse>();
    }
}