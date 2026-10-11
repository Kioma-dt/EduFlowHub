using System.Linq.Expressions;
using EduFlowHub.Application.Filters.Course.Specifications.Abstraction;

namespace EduFlowHub.Application.Filters.Course.Specifications;

public class CourseFilterCategoryNormalizedNameSpecification
    : ICourseFilterSpecification
{
    public CourseFilterType Type { get; } =  CourseFilterType.CategoryNormalizedName;
    public Expression<Func<Domain.Entities.Course, bool>>? ToExpression(object filter) =>
        filter is string categoryName ? course => course.Category != null && course.Category.NormalizedName == categoryName : null;
}