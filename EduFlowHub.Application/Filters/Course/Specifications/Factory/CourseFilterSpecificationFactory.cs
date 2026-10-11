using EduFlowHub.Application.Filters.Course.Specifications.Abstraction;
using EduFlowHub.Application.Filters.Exceptions;

namespace EduFlowHub.Application.Filters.Course.Specifications.Factory;

public class CourseFilterSpecificationFactory
    : ICourseFilterSpecificationFactory
{
    private readonly Dictionary<CourseFilterType, ICourseFilterSpecification> _filters;

    public CourseFilterSpecificationFactory(IEnumerable<ICourseFilterSpecification> filters)
    {
        _filters = filters.ToDictionary(x => x.Type, x => x);
    }
    
    public ICourseFilterSpecification CreateSpecification(CourseFilterType type)
    {
        if (!_filters.TryGetValue(type, out var spec))
        {
            throw new FilterSpecificationNotFoundException(type.ToString());
        }
        return spec;
    }
}