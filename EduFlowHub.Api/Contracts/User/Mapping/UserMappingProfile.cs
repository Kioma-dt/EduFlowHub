using EduFlowHub.Api.Contracts.User.Requests;
using EduFlowHub.Api.Contracts.User.Responses;
using EduFlowHub.Application.Features.User.Commands.Create;
using EduFlowHub.Application.Features.User.Commands.Update;
using EduFlowHub.Application.Features.User.Dtos;

namespace EduFlowHub.Api.Contracts.User.Mapping;

public class UserMappingProfile
    : Profile
{
    public  UserMappingProfile()
    {
        CreateMap<CreateUserRequest, CreateUserCommand>();
        
        CreateMap<UserDto, UserResponse>();
    }
}