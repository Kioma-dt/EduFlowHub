using System.Linq.Expressions;

namespace EduFlowHub.Application.Filters.Course.Specifications.Abstraction;

public interface ICourseFilterSpecification
{
    CourseFilterType Type { get; }
    Expression<Func<Domain.Entities.Course, bool>>? ToExpression(object filter);
}