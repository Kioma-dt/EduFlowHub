using EduFlowHub.Api.Contracts.CurrentUser.Responses;
using EduFlowHub.Application.Features.User.Dtos;

namespace EduFlowHub.Api.Contracts.CurrentUser.Mapping;

public class CurrentUserMappingProfile
    : Profile
{
    public CurrentUserMappingProfile()
    {
        CreateMap<UserDto, CurrentUserResponse>()
            .ForCtorParam(nameof(CurrentUserResponse.UserId), opt => opt.MapFrom(src => src.Id));
    }
}