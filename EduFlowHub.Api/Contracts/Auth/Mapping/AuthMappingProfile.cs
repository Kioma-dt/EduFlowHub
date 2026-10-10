using EduFlowHub.Api.Contracts.Auth.Requests;
using EduFlowHub.Api.Contracts.Auth.Responses;
using EduFlowHub.Application.Features.Authentication.Commands.Register;
using EduFlowHub.Application.Features.Authentication.Dtos;
using EduFlowHub.Application.Features.Authentication.Queries.Login;
using EduFlowHub.Application.Features.User.Dtos;

namespace EduFlowHub.Api.Contracts.Auth.Mapping;

public class AuthMappingProfile
    : Profile
{
    public  AuthMappingProfile()
    {
        CreateMap<LoginRequest, LoginQuery>();
        CreateMap<RegisterRequest, RegisterCommand>();
        
        CreateMap<UserTokenDto, LoginResponse>()
            .ForMember(dst => dst.Id,
                opt 
                    => opt.MapFrom(src => src.User.Id))
            .ForMember(dst => dst.Username,
                opt 
                    => opt.MapFrom(src => src.User.Username))
            .ForMember(dst => dst.Role,
                opt 
                    => opt.MapFrom(src => src.User.Role))
            .ForMember(dst => dst.FirstName,
                opt 
                    => opt.MapFrom(src => src.User.FirstName))
            .ForMember(dst => dst.LastName,
                opt 
                    => opt.MapFrom(src => src.User.LastName));
        CreateMap<UserDto, RegisterResponse>();
    }
}