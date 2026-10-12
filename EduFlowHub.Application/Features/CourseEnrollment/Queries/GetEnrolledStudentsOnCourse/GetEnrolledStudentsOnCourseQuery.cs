using EduFlowHub.Application.Features.Dtos;
using EduFlowHub.Application.Features.User.Dtos;

namespace EduFlowHub.Application.Features.CourseEnrollment.Queries.GetEnrolledStudentsOnCourse;

public record GetEnrolledStudentsOnCourseQuery(Guid CourseId, int Page = 1, int PageSize = 10)
    : IRequest<PagedDto<UserDto>>;