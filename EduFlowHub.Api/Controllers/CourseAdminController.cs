using EduFlowHub.Api.Contracts.CourseAdmin.QueryStrings;
using EduFlowHub.Api.Contracts.CourseAdmin.Requests;
using EduFlowHub.Api.Contracts.CourseAdmin.Responses;
using EduFlowHub.Application.Features.Course.Commands.ChangeAuthor;
using EduFlowHub.Application.Features.Course.Commands.Create;
using EduFlowHub.Application.Features.Course.Commands.Restore;
using EduFlowHub.Application.Features.Course.Commands.Unpublish;
using EduFlowHub.Application.Features.Course.Dtos;
using EduFlowHub.Application.Features.Course.Queries.ListAll;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduFlowHub.Api.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/v1/admin/courses")]
public class CourseAdminController
    : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    
    public  CourseAdminController(
        IMediator mediator,
        IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] GetAllCoursesQueryString filterQuery)
    {
        var query = new ListAllCoursesQuery(
            filterQuery.Category,
            filterQuery.AuthorId,
            filterQuery.Status);
        
        IEnumerable<CourseDto> courses = await _mediator.Send(query);
        var response = _mapper.Map<IEnumerable<CourseAdminResponse>>(courses);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateCourseAdminRequest request)
    {
        var command = _mapper.Map<CreateCourseCommand>(request);
        CourseDto course = await _mediator.Send(command);
        var response = _mapper.Map<CourseAdminResponse>(course);
        return CreatedAtAction(
            actionName: nameof(CourseController.GetById),
            controllerName:  nameof(CourseController),
            routeValues: new { courseId = course.Id },
            response);
    }
    
    [HttpPost("{courseId:guid}/unpublish")]
    public async Task<IActionResult> Unpublish(
        [FromRoute] Guid courseId)
    {
        var command = new UnpublishCourseCommand(courseId);
        await _mediator.Send(command);
        return NoContent();
    }
    
    [HttpPost("{courseId:guid}/restore")]
    public async Task<IActionResult> Restore(
        [FromRoute] Guid courseId)
    {
        var command = new RestoreCourseCommand(courseId);
        await _mediator.Send(command);
        return NoContent();
    }
    
    [HttpPut("{courseId:guid}/author")]
    public async Task<IActionResult> ChangeAuthor(
        [FromRoute] Guid courseId,
        [FromBody] ChangeCourseAuthorRequest request)
    {
        var command = new ChangeCourseAuthorCommand(
            courseId,
            request.AuthorId);
        await _mediator.Send(command);
        return NoContent();
    }
}