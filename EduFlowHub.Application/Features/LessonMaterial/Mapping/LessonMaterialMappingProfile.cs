using EduFlowHub.Application.Features.LessonMaterial.Dtos;

namespace EduFlowHub.Application.Features.LessonMaterial.Mapping;

public class LessonMaterialMappingProfile
    : Profile
{
    public LessonMaterialMappingProfile()
    {
        CreateMap<Domain.Entities.LessonMaterial, LessonMaterialDto>();
        CreateMap<FileMetadata, FileMetadataDto>();
    }
}