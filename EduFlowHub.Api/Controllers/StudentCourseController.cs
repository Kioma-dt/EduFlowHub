using EduFlowHub.Api.Contracts.Course.Responses;
using EduFlowHub.Application.Abstractions.Services.User;
using EduFlowHub.Application.Features.Course.Dtos;
using EduFlowHub.Application.Features.Course.Queries.ListEnrollerd;
using EduFlowHub.Application.Features.CourseEnrollment.Commands.EnrollStudentOnCourse;
using EduFlowHub.Application.Features.CourseEnrollment.Commands.UnenrollStudentFromCourse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduFlowHub.Api.Controllers;

[ApiController]
[Authorize(Roles = "Student")]
[Route("api/v1/courses")]
public class StudentCourseController
    : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;
    
    public  StudentCourseController(
        IMediator mediator,
        IMapper mapper,
        ICurrentUser currentUser)
    {
        _mediator = mediator;
        _mapper = mapper;
        _currentUser = currentUser;
    }
    
    [Authorize(Roles = "Student")]
    [HttpGet("enrolled")]
    public async Task<ActionResult<IEnumerable<CourseResponse>>> GetEnrolled(CancellationToken cancellationToken)
    {
        var query = new ListEnrolledCoursesQuery(_currentUser.CurrentUserId);
        IEnumerable<CourseDto> courses = await _mediator.Send(query, cancellationToken);
        var response = _mapper.Map<IEnumerable<CourseResponse>>(courses);
        return Ok(response);
    }
    
    [HttpPost("{courseId:guid}/enroll")]
    public async Task<IActionResult> EnrollOnCourse(
        [FromRoute] Guid courseId,
        CancellationToken cancellationToken)
    {
        var command = new EnrollStudentOnCourseCommand(
            _currentUser.CurrentUserId,
            courseId);
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
    
    [HttpPost("{courseId:guid}/unenroll")]
    public async Task<IActionResult> UnenrollFromCourse(
        [FromRoute] Guid courseId,
        CancellationToken cancellationToken)
    {
        var command = new UnenrollStudentFromCourseCommand(
            _currentUser.CurrentUserId,
            courseId);
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
    
}