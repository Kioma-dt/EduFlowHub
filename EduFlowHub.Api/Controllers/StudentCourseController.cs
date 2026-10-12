using EduFlowHub.Api.Contracts.Course.QueryStrings;
using EduFlowHub.Api.Contracts.Course.Responses;
using EduFlowHub.Api.Contracts.Generic.QueryStrings;
using EduFlowHub.Api.Contracts.Generic.Responses;
using EduFlowHub.Application.Abstractions.Services.User;
using EduFlowHub.Application.Features.Course.Dtos;
using EduFlowHub.Application.Features.Course.Queries.ListEnrolledPublished;
using EduFlowHub.Application.Features.CourseEnrollment.Commands.EnrollStudentOnCourse;
using EduFlowHub.Application.Features.CourseEnrollment.Commands.UnenrollStudentFromCourse;
using EduFlowHub.Application.Features.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduFlowHub.Api.Controllers;

[ApiController]
[Authorize(Roles = "Student")]
[ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
[ProducesResponseType<ProblemDetails>(StatusCodes.Status401Unauthorized)]
[ProducesResponseType<ProblemDetails>(StatusCodes.Status403Forbidden)]
[ProducesResponseType<ProblemDetails>(StatusCodes.Status413PayloadTooLarge)]
[ProducesResponseType<ProblemDetails>(StatusCodes.Status429TooManyRequests)]
[ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
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
    
    [ProducesResponseType<PagedResponse<PagedResponse<CourseResponse>>>(StatusCodes.Status200OK)]
    [HttpGet("enrolled")]
    public async Task<ActionResult<PagedResponse<CourseResponse>>> GetEnrolled(
        [FromQuery] GetStudentEnrolledCoursesQueryString filterQueryString,
        [FromQuery] PageQueryString pageQueryString,
        CancellationToken cancellationToken)
    {
        var query = new ListEnrolledCoursesPublishedQuery(
            _currentUser.CurrentUserId, 
            filterQueryString.CategoryNormalizedName, 
            filterQueryString.AuthorId,
            pageQueryString.Page,
            pageQueryString.PageSize);
        PagedDto<CourseDto> result = await _mediator.Send(query, cancellationToken);
        var items = _mapper.Map<IEnumerable<CourseResponse>>(result.Items);
        var response = new PagedResponse<CourseResponse>(items, result.Page, result.PageSize,result.TotalCount);
        return Ok(response);
    }
    
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
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
    
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
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