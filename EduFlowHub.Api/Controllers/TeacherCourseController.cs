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
using EduFlowHub.Application.Features.Course.Queries.GetByIdForAuthor;
using EduFlowHub.Application.Features.Course.Queries.ListOfAuthor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduFlowHub.Api.Controllers;

[ApiController]
[Authorize(Roles = "Teacher")]
[Route("api/v1/courses/teachers/me")]
public class TeacherCourseController
    :ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;
    
    public  TeacherCourseController(
        IMediator mediator,
        IMapper mapper,
        ICurrentUser currentUser)
    {
        _mediator = mediator;
        _mapper = mapper;
        _currentUser = currentUser;
    }
    
    [HttpGet]
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
    
    [HttpGet("{courseId:guid}")]
    public async Task<ActionResult<CourseResponse>> GetById(
        [FromRoute] Guid courseId,
        CancellationToken cancellationToken)
    {
        var query = new GetCourseByIdForAuthorQuery(
            courseId,
            _currentUser.CurrentUserId);
        CourseDto course = await _mediator.Send(query, cancellationToken);
        var response = _mapper.Map<CourseResponse>(course);
        return Ok(response);
    }

    
    [HttpPost]
    public async Task<ActionResult<CourseResponse>> CreateCourse(
        [FromBody] CreateMyCourseRequest request,
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
    
    [HttpDelete("{courseId:guid}")]
    public async Task<IActionResult> DeleteCourse(
        [FromRoute] Guid courseId,
        CancellationToken cancellationToken)
    {
        var command = new DeleteCourseCommand(courseId);
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
    
    [HttpPatch("{courseId:guid}")]
    public async Task<IActionResult> UpdateCourse(
        [FromRoute] Guid courseId,
        [FromBody] UpdateMyCourseRequest request,
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

    [HttpPost("{courseId:guid}/publish")]
    public async Task<IActionResult> Publish(
        [FromRoute] Guid courseId,
        CancellationToken cancellationToken)
    {
        var command = new PublishCourseCommand(courseId);
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpPost("{courseId:guid}/archive")]
    public async Task<IActionResult> Archive(
        [FromRoute] Guid courseId,
        CancellationToken cancellationToken)
    {
        var command = new ArchiveCourseCommand(courseId);
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
}