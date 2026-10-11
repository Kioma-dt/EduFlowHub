using System.Linq.Expressions;
using EduFlowHub.Application.Filters.Course.Specifications.Abstraction;

namespace EduFlowHub.Application.Filters.Course.Specifications;

public class CourseFilterAuthorIdSpecification
    : ICourseFilterSpecification
{
    public CourseFilterType Type { get; } =  CourseFilterType.AuthorId;
    public Expression<Func<Domain.Entities.Course, bool>>? ToExpression(object filter) =>
        filter is Guid authorId ? course => course.AuthorId == authorId : null;
}