using EduFlowHub.Application.Features.Course.Dtos;

namespace EduFlowHub.Application.Features.Course.Mapping;

public class CourseMappingProfile
    : Profile
{
    public  CourseMappingProfile()
    {
        CreateMap<Domain.Entities.Course, CourseDto>()
            .ForMember(dest => dest.AuthorName, 
                opt => opt.MapFrom(src => src.Author.Name))
            .ForMember(dest => dest.CategoryName,
                opt => opt.MapFrom(src => src.Category.Name));
    }
}