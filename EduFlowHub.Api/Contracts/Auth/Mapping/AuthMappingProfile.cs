using EduFlowHub.Api.Contracts.Auth.Requests;
using EduFlowHub.Api.Contracts.Auth.Responses;
using EduFlowHub.Application.Features.Authentication.Commands.Login;
using EduFlowHub.Application.Features.Authentication.Commands.Register;
using EduFlowHub.Application.Features.Authentication.Dtos;
using EduFlowHub.Application.Features.User.Dtos;

namespace EduFlowHub.Api.Contracts.Auth.Mapping;

public class AuthMappingProfile
    : Profile
{
    public  AuthMappingProfile()
    {
        CreateMap<LoginRequest, LoginCommand>();
        CreateMap<RegisterRequest, RegisterCommand>();
        
        CreateMap<UserTokenDto, LoginResponse>()
            .ForCtorParam(nameof(LoginResponse.Id),        o => o.MapFrom(s => s.User.Id))
            .ForCtorParam(nameof(LoginResponse.Username),  o => o.MapFrom(s => s.User.Username))
            .ForCtorParam(nameof(LoginResponse.Role),      o => o.MapFrom(s => s.User.Role))
            .ForCtorParam(nameof(LoginResponse.FirstName), o => o.MapFrom(s => s.User.FirstName))
            .ForCtorParam(nameof(LoginResponse.LastName),  o => o.MapFrom(s => s.User.LastName));
        CreateMap<UserDto, RegisterResponse>();
    }
}