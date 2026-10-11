using EduFlowHub.Application.Filters.Course.Specifications.Abstraction;

namespace EduFlowHub.Application.Filters.Course.Specifications.Factory;

public interface ICourseFilterSpecificationFactory
{
    ICourseFilterSpecification CreateSpecification(CourseFilterType type);
}