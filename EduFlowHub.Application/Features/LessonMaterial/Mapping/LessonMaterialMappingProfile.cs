using EduFlowHub.Application.Features.LessonMaterial.Dtos;

namespace EduFlowHub.Application.Features.LessonMaterial.Mapping;

public class LessonMaterialMappingProfile
    : Profile
{
    public LessonMaterialMappingProfile()
    {
        CreateMap<Domain.Entities.LessonMaterial, LessonMaterialDto>()
            .ForCtorParam(nameof(LessonMaterialDto.MaterialType), opt => opt.MapFrom(src => src.Type));
        CreateMap<FileMetadata, FileMetadataDto>();
    }
}