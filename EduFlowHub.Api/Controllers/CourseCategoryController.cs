using EduFlowHub.Api.Contracts.CourseCategory.Requests;
using EduFlowHub.Api.Contracts.CourseCategory.Responses;
using EduFlowHub.Application.Features.CourseCategory.Commands.Create;
using EduFlowHub.Application.Features.CourseCategory.Commands.Delete;
using EduFlowHub.Application.Features.CourseCategory.Commands.Update;
using EduFlowHub.Application.Features.CourseCategory.Dtos;
using EduFlowHub.Application.Features.CourseCategory.Queries.GetById;
using EduFlowHub.Application.Features.CourseCategory.Queries.ListAll;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduFlowHub.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/course-categories")]
public class CourseCategoryController
    : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    
    public  CourseCategoryController(
        IMediator mediator,
        IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var query = new ListAllCourseCategoriesQuery();
        IEnumerable<CourseCategoryDto> result = await _mediator.Send(query, cancellationToken);
        var response = _mapper.Map<IEnumerable<CourseCategoryResponse>>(result);
        return Ok(response);
    }

    [HttpGet("{courseCategoryId:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid courseCategoryId,
        CancellationToken cancellationToken)
    {
        var query = new GetCourseCategoryByIdQuery(courseCategoryId);
        CourseCategoryDto result = await _mediator.Send(query, cancellationToken);
        var response =  _mapper.Map<CourseCategoryResponse>(result);
        return Ok(response);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateCourseCategoryRequest request,
        CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateCourseCategoryCommand>(request);
        CourseCategoryDto result = await _mediator.Send(command, cancellationToken);
        var response =  _mapper.Map<CourseCategoryResponse>(result);
        return CreatedAtAction(
            nameof(GetById), 
            new { courseCategoryId = response.Id }, 
            response);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPut("{courseCategoryId:guid}")]
    public async Task<IActionResult> Put(
        [FromRoute] Guid courseCategoryId,
        [FromBody] PutCourseCategoryRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateCourseCategoryCommand(
            courseCategoryId,
            request.Name); 
        CourseCategoryDto result =  await _mediator.Send(command, cancellationToken);
        var response =  _mapper.Map<CourseCategoryResponse>(result);
        
        return Ok(response);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpDelete("{courseCategoryId:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid courseCategoryId,
        CancellationToken cancellationToken)
    {
        var command = new DeleteCourseCategoryCommand(courseCategoryId);
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
}