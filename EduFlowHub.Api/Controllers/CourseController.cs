using EduFlowHub.Api.Contracts.Course.QueryStrings;
using EduFlowHub.Api.Contracts.Course.Requests;
using EduFlowHub.Api.Contracts.Course.Responses;
using EduFlowHub.Api.Contracts.Generic.QueryStrings;
using EduFlowHub.Api.Contracts.Generic.Responses;
using EduFlowHub.Api.Contracts.User.Responses;
using EduFlowHub.Application.Abstractions.Services.User;
using EduFlowHub.Application.Features.Course.Commands.Archive;
using EduFlowHub.Application.Features.Course.Commands.ChangeAuthor;
using EduFlowHub.Application.Features.Course.Commands.Create;
using EduFlowHub.Application.Features.Course.Commands.Delete;
using EduFlowHub.Application.Features.Course.Commands.Publish;
using EduFlowHub.Application.Features.Course.Commands.Restore;
using EduFlowHub.Application.Features.Course.Commands.Unpublish;
using EduFlowHub.Application.Features.Course.Commands.Update;
using EduFlowHub.Application.Features.Course.Dtos;
using EduFlowHub.Application.Features.Course.Queries.GetById;
using EduFlowHub.Application.Features.Course.Queries.ListAll;
using EduFlowHub.Application.Features.Course.Queries.ListPublished;
using EduFlowHub.Application.Features.CourseEnrollment.Commands.EnrollStudentOnCourse;
using EduFlowHub.Application.Features.CourseEnrollment.Commands.UnenrollStudentFromCourse;
using EduFlowHub.Application.Features.CourseEnrollment.Queries.GetEnrolledStudentsOnCourse;
using EduFlowHub.Application.Features.Dtos;
using EduFlowHub.Application.Features.User.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduFlowHub.Api.Controllers;

[Authorize]
[ApiController]
[ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
[ProducesResponseType<ProblemDetails>(StatusCodes.Status401Unauthorized)]
[ProducesResponseType<ProblemDetails>(StatusCodes.Status403Forbidden)]
[ProducesResponseType<ProblemDetails>(StatusCodes.Status413PayloadTooLarge)]
[ProducesResponseType<ProblemDetails>(StatusCodes.Status429TooManyRequests)]
[ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
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
    
    [ProducesResponseType<PagedResponse<CourseResponse>>(StatusCodes.Status200OK)]
    [HttpGet("published")]
    public async Task<ActionResult<PagedResponse<CourseResponse>>> GetPublished(
        [FromQuery] GetPublishedCoursesQueryString filterQueryString,
        [FromQuery] PageQueryString pageQueryString,
        CancellationToken cancellationToken)
    {
        var query = new ListPublishedCoursesQuery(
                filterQueryString.AuthorId,
                filterQueryString.CategoryNormalizedName,
                pageQueryString.Page,
                pageQueryString.PageSize);
        PagedDto<CourseDto> result = await _mediator.Send(query, cancellationToken);
        var items = _mapper.Map<IEnumerable<CourseResponse>>(result.Items);
        var response = new PagedResponse<CourseResponse>(items, result.Page, result.PageSize, result.TotalCount);
        return Ok(response);
    }
    
    [Authorize(Roles = "Admin")]
    [ProducesResponseType<PagedResponse<CourseResponse>>(StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<ActionResult<PagedResponse<CourseResponse>>> Get(
        [FromQuery] GetAllCoursesQueryString filterQuery,
        [FromQuery] PageQueryString pageQueryString,
        CancellationToken cancellationToken)
    {
        var query = new ListAllCoursesQuery(
            filterQuery.CategoryNormalizedName,
            filterQuery.AuthorId,
            filterQuery.Status,
            pageQueryString.Page,
            pageQueryString.PageSize);
        PagedDto<CourseDto> result = await _mediator.Send(query, cancellationToken);
        var items = _mapper.Map<IEnumerable<CourseResponse>>(result.Items);
        var response = new PagedResponse<CourseResponse>(items, result.Page, result.PageSize, result.TotalCount);
        return Ok(response);
    }

    [Authorize(Roles = "Admin")]
    [ProducesResponseType<CourseResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
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
    
    [Authorize(Roles = "Admin")]
    [ProducesResponseType<CourseResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
    [HttpPost]
    public async Task<ActionResult<CourseResponse>> Create(
        [FromBody] CreateCourseRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateCourseCommand(
            request.Title,
            request.Description,
            request.Price,
            request.CategoryId,
            request.AuthorId);
        CourseDto course = await _mediator.Send(command, cancellationToken);
        var response = _mapper.Map<CourseResponse>(course);
        return CreatedAtAction(
            nameof(GetById),
            routeValues: new { courseId = course.Id },
            response);
    }
    
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
    [HttpPatch("{courseId:guid}")]
    public async Task<IActionResult> Update(
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
    
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
    [HttpDelete("{courseId:guid}")]
    public async Task<IActionResult> DeleteCourse(
        [FromRoute] Guid courseId,
        CancellationToken cancellationToken)
    {
        var command = new DeleteCourseCommand(courseId);
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
    
        
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
    [HttpPut("{courseId:guid}/author")]
    public async Task<IActionResult> ChangeAuthor(
        [FromRoute] Guid courseId,
        [FromBody] ChangeCourseAuthorRequest request,
        CancellationToken cancellationToken)
    {
        var command = new ChangeCourseAuthorCommand(
            courseId,
            request.AuthorId);
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }

    
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
    [HttpPost("{courseId:guid}/unpublish")]
    public async Task<IActionResult> Unpublish(
        [FromRoute] Guid courseId,
        CancellationToken cancellationToken)
    {
        var command = new UnpublishCourseCommand(courseId);
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
    
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
    [HttpPost("{courseId:guid}/restore")]
    public async Task<IActionResult> Restore(
        [FromRoute] Guid courseId,
        CancellationToken cancellationToken)
    {
        var command = new RestoreCourseCommand(courseId);
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
    
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
    [HttpPost("{courseId:guid}/publish")]
    public async Task<IActionResult> Publish(
        [FromRoute] Guid courseId,
        CancellationToken cancellationToken)
    {
        var command = new PublishCourseCommand(courseId);
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
    
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
    [HttpPost("{courseId:guid}/archive")]
    public async Task<IActionResult> Archive(
        [FromRoute] Guid courseId,
        CancellationToken cancellationToken)
    {
        var command = new ArchiveCourseCommand(courseId);
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [ProducesResponseType<PagedResponse<UserResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [HttpGet("{courseId:guid}/students/enrolled")]
    public async Task<ActionResult<PagedResponse<UserResponse>>> GetEnrolledStudents(
        [FromRoute] Guid courseId,
        [FromQuery] PageQueryString pageQueryString,
        CancellationToken cancellationToken)
    {
        var command = new GetEnrolledStudentsOnCourseQuery(courseId, pageQueryString.Page, pageQueryString.PageSize);
        PagedDto<UserDto> result = await _mediator.Send(command, cancellationToken);
        var items = _mapper.Map<IEnumerable<UserResponse>>(result.Items);
        var response = new PagedResponse<UserResponse>(items, result.Page, result.PageSize, result.TotalCount);
        return Ok(response);
    }
    
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
    [HttpPost("{courseId:guid}/students/{studentId:guid}/enroll")]
    public async Task<IActionResult> EnrollStudentOnCourse(
        [FromRoute] Guid courseId,
        [FromRoute] Guid studentId,
        CancellationToken cancellationToken)
    {
        var command = new EnrollStudentOnCourseCommand(studentId, courseId);
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
    
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
    [HttpPost("{courseId:guid}/students/{studentId:guid}/unenroll")]
    public async Task<IActionResult> UnenrollStudentOnCourse(
        [FromRoute] Guid courseId,
        [FromRoute] Guid studentId,
        CancellationToken cancellationToken)
    {
        var command = new UnenrollStudentFromCourseCommand(studentId, courseId);
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
}