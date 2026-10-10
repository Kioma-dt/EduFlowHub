using EduFlowHub.Api.Contracts.Course.QueryStrings;
using EduFlowHub.Api.Contracts.Course.Requests;
using EduFlowHub.Api.Contracts.Course.Responses;
using EduFlowHub.Application.Abstractions.Services;
using EduFlowHub.Application.Features.Course.Commands.Create;
using EduFlowHub.Application.Features.Course.Commands.Delete;
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
    public async Task<IActionResult> GetById(
        [FromRoute] Guid courseId)
    {
        var query = new GetCourseByIdQuery(courseId);
        CourseDto course = await _mediator.Send(query);
        var response = _mapper.Map<CourseDto>(course);
        return Ok(response);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetPublished(
        [FromQuery] GetPublishedCoursesQueryString filterQuery)
    {
        var query = new ListPublishedCoursesQuery(
            filterQuery.AuthorId,
            filterQuery.Category);
        IEnumerable<CourseDto> courses = await _mediator.Send(query);
        var response = _mapper.Map<IEnumerable<CourseResponse>>(courses);
        return Ok(response);
    }
    
    [Authorize(Roles = "Teacher")]
    [HttpGet("my")]
    public async Task<IActionResult> Get(
        [FromQuery] GetMyCoursesQueryString filterQuery)
    {
        var query = new ListCoursesOfAuthorQuery(
            _currentUser.CurrentUserId,
            filterQuery.Category,
            filterQuery.Status);
        IEnumerable<CourseDto> courses = await _mediator.Send(query);
        var response = _mapper.Map<IEnumerable<CourseResponse>>(courses);
        return Ok(response);
    }

    [Authorize(Roles = "Teacher")]
    [HttpPost]
    public async Task<IActionResult> CreateCourse(
        [FromBody] CreateCourseRequest request)
    {
        var command = new CreateCourseCommand(
            request.Title,
            request.Description,
            request.Price,
            _currentUser.CurrentUserId,
            request.CategoryId);
        CourseDto result = await _mediator.Send(command);
        var response = _mapper.Map<CourseResponse>(result);
        
        return CreatedAtAction(nameof(GetById),
            new { courseId = response.Id }, 
            response);
    }

    [Authorize(Roles = "Admin, Teacher")]
    [HttpPatch("{courseId:guid}")]
    public async Task<IActionResult> UpdateCourse(
        [FromRoute] Guid courseId,
        [FromBody] UpdateCourseRequest request)
    {
        var command = new UpdateCourseCommand(
            courseId,
            request.Title,
            request.Description,
            request.Price,
            request.CategoryId);
        await _mediator.Send(command);
        return NoContent();
    }
    
    [Authorize(Roles = "Admin, Teacher")]
    [HttpDelete("{courseId:guid}")]
    public async Task<IActionResult> DeleteCourse(
        [FromRoute] Guid courseId)
    {
        var command = new DeleteCourseCommand(courseId);
        await _mediator.Send(command);
        return NoContent();
    }

    [Authorize(Roles = "Student")]
    [HttpGet("enrolled")]
    public async Task<IActionResult> GetEnrolled()
    {
        var query = new ListEnrolledCoursesQuery(_currentUser.CurrentUserId);
        IEnumerable<CourseDto> courses = await _mediator.Send(query);
        var response = _mapper.Map<IEnumerable<CourseResponse>>(courses);
        return Ok(response);
    }
    
    [Authorize(Roles = "Student")]
    [HttpPost("{courseId:guid}/enroll")]
    public async Task<IActionResult> EnrollCourse(
        [FromRoute] Guid courseId)
    {
        var command = new EnrollStudentOnCourseCommand(
            _currentUser.CurrentUserId,
            courseId);
        await _mediator.Send(command);
        return NoContent();
    }
}