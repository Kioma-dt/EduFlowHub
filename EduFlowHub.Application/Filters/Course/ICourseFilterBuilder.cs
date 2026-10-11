using System.Linq.Expressions;
using EduFlowHub.Application.Extensions;
using EduFlowHub.Application.Filters.Course.Specifications.Abstraction;
using EduFlowHub.Application.Filters.Course.Specifications.Factory;

namespace EduFlowHub.Application.Filters.Course;

public interface ICourseFilterBuilder
{
    Expression<Func<Domain.Entities.Course, bool>> BuildFilter(IEnumerable<CourseFilter> filters);
}

public class CourseFilterBuilder
    : ICourseFilterBuilder
{
    private readonly ICourseFilterSpecificationFactory _filterSpecificationFactory;

    private static readonly Expression<Func<Domain.Entities.Course, bool>> AlwaysTrue = course => true;

    public CourseFilterBuilder(ICourseFilterSpecificationFactory filterSpecificationFactory)
    {
        _filterSpecificationFactory = filterSpecificationFactory;
    }
    
    public Expression<Func<Domain.Entities.Course, bool>> BuildFilter(IEnumerable<CourseFilter> filters)
    {
        Expression<Func<Domain.Entities.Course, bool>>? expression = null;

        foreach (var filter in filters)
        {
            var specification = _filterSpecificationFactory.CreateSpecification(filter.Type);
            var nextExpression = specification.ToExpression(filter.Value);
            if (expression is null)
            {
                expression = nextExpression;
            }
            else if(nextExpression is not null)
            {
                expression = expression.And(nextExpression);
            }
        }

        return expression ?? AlwaysTrue;
    }
}