using EduFlowHub.Application.Features.User.Dtos;

namespace EduFlowHub.Application.Features.User.Mapping;

public class UserMappingProfile
    : Profile
{
    public UserMappingProfile()
    {
        CreateMap<Domain.Entities.User, UserDto>();
    }
}