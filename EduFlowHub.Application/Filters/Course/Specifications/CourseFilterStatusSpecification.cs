using System.Linq.Expressions;
using EduFlowHub.Application.Filters.Course.Specifications.Abstraction;

namespace EduFlowHub.Application.Filters.Course.Specifications;

public class CourseFilterStatusSpecification
    : ICourseFilterSpecification
{
    public CourseFilterType Type { get; } =  CourseFilterType.Status;
    public Expression<Func<Domain.Entities.Course, bool>>? ToExpression(object filter) =>
        filter is CourseStatus status ? course => course.Status == status : null;
}