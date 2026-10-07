using EduFlowHub.Application.Features.CourseCategory.Dtos;

namespace EduFlowHub.Application.Features.CourseCategory.Mapping;

public class CourseCategoryMappingProfile
    : Profile
{
    public CourseCategoryMappingProfile()
    {
        CreateMap<Domain.Entities.CourseCategory, CourseCategoryDto>();
    }
}