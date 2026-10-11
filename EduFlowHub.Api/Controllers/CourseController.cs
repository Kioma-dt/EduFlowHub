using EduFlowHub.Api.Contracts.Course.QueryStrings;
using EduFlowHub.Api.Contracts.Course.Requests;
using EduFlowHub.Api.Contracts.Course.Responses;
using EduFlowHub.Application.Abstractions.Services.User;
using EduFlowHub.Application.Features.Course.Commands.Archive;
using EduFlowHub.Application.Features.Course.Commands.Create;
using EduFlowHub.Application.Features.Course.Commands.Delete;
using EduFlowHub.Application.Features.Course.Commands.Publish;
using EduFlowHub.Application.Features.Course.Commands.Update;
using EduFlowHub.Application.Features.Course.Dtos;
using EduFlowHub.Application.Features.Course.Queries.GetById;
using EduFlowHub.Application.Features.Course.Queries.ListEnrollerd;
using EduFlowHub.Application.Features.Course.Queries.ListOfAuthor;
using EduFlowHub.Application.Features.Course.Queries.ListPublished;
using EduFlowHub.Application.Features.CourseEnrollment.Commands.EnrollStudentOnCourse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduFlowHub.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/courses")]
public class CourseController
    : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;
    
    public  CourseController(
        IMediator mediator,
        IMapper mapper,
        ICurrentUser currentUser)
    {
        _mediator = mediator;
        _mapper = mapper;
        _currentUser = currentUser;
    }

    [Authorize(Roles = "Admin, Teacher")]
    [HttpGet("{courseId:guid}")]
    public async Task<ActionResult<CourseResponse>> GetById(
        [FromRoute] Guid courseId,
        CancellationToken cancellationToken)
    {
        var query = new GetCourseByIdQuery(courseId);
        CourseDto course = await _mediator.Send(query, cancellationToken);
        var response = _mapper.Map<CourseResponse>(course);
        return Ok(response);
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CourseResponse>>> GetPublished(
        [FromQuery] GetPublishedCoursesQueryString filterQuery,
        CancellationToken cancellationToken)
    {
        var query = new ListPublishedCoursesQuery(
            filterQuery.AuthorId,
            filterQuery.Category);
        IEnumerable<CourseDto> courses = await _mediator.Send(query, cancellationToken);
        var response = _mapper.Map<IEnumerable<CourseResponse>>(courses);
        return Ok(response);
    }
    
    [Authorize(Roles = "Teacher")]
    [HttpGet("my")]
    public async Task<ActionResult<IEnumerable<CourseResponse>>> Get(
        [FromQuery] GetMyCoursesQueryString filterQuery,
        CancellationToken cancellationToken)
    {
        var query = new ListCoursesOfAuthorQuery(
            _currentUser.CurrentUserId,
            filterQuery.Category,
            filterQuery.Status);
        IEnumerable<CourseDto> courses = await _mediator.Send(query, cancellationToken);
        var response = _mapper.Map<IEnumerable<CourseResponse>>(courses);
        return Ok(response);
    }

    [Authorize(Roles = "Teacher")]
    [HttpPost]
    public async Task<ActionResult<CourseResponse>> CreateCourse(
        [FromBody] CreateCourseRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateCourseCommand(
            request.Title,
            request.Description,
            request.Price,
            request.CategoryId,
            _currentUser.CurrentUserId);
        CourseDto result = await _mediator.Send(command, cancellationToken);
        var response = _mapper.Map<CourseResponse>(result);
        
        return CreatedAtAction(nameof(GetById),
            new { courseId = response.Id }, 
            response);
    }

    [Authorize(Roles = "Admin, Teacher")]
    [HttpPatch("{courseId:guid}")]
    public async Task<IActionResult> UpdateCourse(
        [FromRoute] Guid courseId,
        [FromBody] UpdateCourseRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateCourseCommand(
            courseId,
            request.Title,
            request.Description,
            request.Price,
            request.CategoryId);
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
    
    [Authorize(Roles = "Admin, Teacher")]
    [HttpPost("{courseId:guid}/publish")]
    public async Task<IActionResult> Publish(
        [FromRoute] Guid courseId,
        CancellationToken cancellationToken)
    {
        var command = new PublishCourseCommand(courseId);
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
    
    [Authorize(Roles = "Admin, Teacher")]
    [HttpPost("{courseId:guid}/archive")]
    public async Task<IActionResult> Archive(
        [FromRoute] Guid courseId,
        CancellationToken cancellationToken)
    {
        var command = new ArchiveCourseCommand(courseId);
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
    
    [Authorize(Roles = "Admin, Teacher")]
    [HttpDelete("{courseId:guid}")]
    public async Task<IActionResult> DeleteCourse(
        [FromRoute] Guid courseId,
        CancellationToken cancellationToken)
    {
        var command = new DeleteCourseCommand(courseId);
        await _mediator.Send(command, cancellationToken);
        return NoContent();
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
    
    [Authorize(Roles = "Student")]
    [HttpPost("{courseId:guid}/enroll")]
    public async Task<IActionResult> EnrollCourse(
        [FromRoute] Guid courseId,
        CancellationToken cancellationToken)
    {
        var command = new EnrollStudentOnCourseCommand(
            _currentUser.CurrentUserId,
            courseId);
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
}