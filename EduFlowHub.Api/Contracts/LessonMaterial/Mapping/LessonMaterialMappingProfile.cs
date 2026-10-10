using EduFlowHub.Api.Contracts.LessonMaterial.Responses;
using EduFlowHub.Application.Features.LessonMaterial.Dtos;

namespace EduFlowHub.Api.Contracts.LessonMaterial.Mapping;

public class LessonMaterialMappingProfile
    : Profile
{
    public LessonMaterialMappingProfile()
    {
        CreateMap<FileMetadataDto, FileMetadataResponse>();
        CreateMap<LessonMaterialDto, LessonMaterialResponse>();
    }
}