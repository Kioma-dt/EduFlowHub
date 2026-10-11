using EduFlowHub.Application.Features.User.Dtos;

namespace EduFlowHub.Application.Features.CourseEnrollment.Queries.GetEnrolledStudentsOnCourse;

public record GetEnrolledStudentsOnCourseQuery(Guid CourseId)
    : IRequest<IEnumerable<UserDto>>;