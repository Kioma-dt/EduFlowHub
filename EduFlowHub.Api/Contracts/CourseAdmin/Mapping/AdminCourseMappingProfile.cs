using EduFlowHub.Api.Contracts.CourseAdmin.Requests;
using EduFlowHub.Api.Contracts.CourseAdmin.Responses;
using EduFlowHub.Application.Features.Course.Commands.Create;
using EduFlowHub.Application.Features.Course.Dtos;
using EduFlowHub.Application.Features.CourseCategory.Dtos;
using EduFlowHub.Application.Features.User.Dtos;

namespace EduFlowHub.Api.Contracts.CourseAdmin.Mapping;

public class AdminCourseMappingProfile
    : Profile
{
    public AdminCourseMappingProfile()
    {
        CreateMap<CreateCourseAdminRequest, CreateCourseCommand>();

        CreateMap<CourseCategoryDto, CategoryOfCourseAdminResponse>();
        CreateMap<UserDto, AuthorOfCourseAdminResponse>();
        CreateMap<CourseDto, CourseAdminResponse>();
    }
}