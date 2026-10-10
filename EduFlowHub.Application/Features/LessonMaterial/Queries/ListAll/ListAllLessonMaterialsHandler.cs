using EduFlowHub.Application.Abstractions.Persistence;
using EduFlowHub.Application.Features.LessonMaterial.Dtos;

namespace EduFlowHub.Application.Features.LessonMaterial.Queries.ListAll;

public class ListAllLessonMaterialsHandler
    : IRequestHandler<ListAllLessonMaterialsQuery, IEnumerable<LessonMaterialDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public ListAllLessonMaterialsHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<LessonMaterialDto>> Handle(
        ListAllLessonMaterialsQuery request,
        CancellationToken cancellationToken)
    {
        Func<IQueryable<Domain.Entities.LessonMaterial>, IOrderedQueryable<Domain.Entities.LessonMaterial>>? orderBy= null;
        if (request.OrderByNumber)
        {
            orderBy = lessonMaterials 
                => lessonMaterials
                    .OrderBy(lessonMaterial =>  lessonMaterial.OrderNumber);
        }
        
        IEnumerable<Domain.Entities.LessonMaterial> lessonMaterials = await _unitOfWork
            .LessonMaterialRepository
            .GetAllAsync(null, orderBy, cancellationToken);
        
        return _mapper.Map<IEnumerable<LessonMaterialDto>>(lessonMaterials);
    }
}