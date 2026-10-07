using EduFlowHub.Application.Features.Course.Dtos;

namespace EduFlowHub.Application.Features.Course.Mapping;

public class CourseMappingProfile
    : Profile
{
    public  CourseMappingProfile()
    {
        CreateMap<Domain.Entities.Course, CourseDto>();
    }
}