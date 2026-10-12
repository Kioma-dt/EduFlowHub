using System.Linq.Expressions;
using EduFlowHub.Application.Filters.Course.Specifications.Abstraction;

namespace EduFlowHub.Application.Filters.Course;

public interface ICourseFilterBuilder
{
    Expression<Func<Domain.Entities.Course, bool>> BuildFilter(IEnumerable<CourseFilter> filters);
}