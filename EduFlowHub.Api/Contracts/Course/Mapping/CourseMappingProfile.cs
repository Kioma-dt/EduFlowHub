using EduFlowHub.Api.Contracts.Course.Requests;
using EduFlowHub.Api.Contracts.Course.Responses;
using EduFlowHub.Application.Features.Course.Commands.Create;
using EduFlowHub.Application.Features.Course.Dtos;
using EduFlowHub.Application.Features.CourseCategory.Dtos;
using EduFlowHub.Application.Features.User.Dtos;

namespace EduFlowHub.Api.Contracts.Course.Mapping;

public class CourseMappingProfile
    : Profile
{
    public CourseMappingProfile()
    {
        CreateMap<CourseCategoryDto, CategoryOfCourseResponse>();
        CreateMap<UserDto, AuthorOfCourseResponse>();
        CreateMap<CourseDto, CourseResponse>();
    }
}