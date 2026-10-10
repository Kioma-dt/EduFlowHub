namespace EduFlowHub.Application.Features.CourseEnrollment.Queries.IsStudentEnrolledOnCourse;

public sealed  record IsStudentEnrolledOnCourseQuery(Guid StudentId, Guid CourseId)
    : IRequest<bool>;