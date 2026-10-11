using System.Linq.Expressions;
using EduFlowHub.Application.Filters.Course.Specifications.Abstraction;

namespace EduFlowHub.Application.Filters.Course.Specifications;

public class CourseFilterCategoryIdSpecification
    : ICourseFilterSpecification
{
    public CourseFilterType Type { get; } =  CourseFilterType.CategoryId;
    public Expression<Func<Domain.Entities.Course, bool>>? ToExpression(object filter) =>
        filter is Guid categoryId ? course => course.CategoryId == categoryId : null;
}