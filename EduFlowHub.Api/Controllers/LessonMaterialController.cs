using EduFlowHub.Api.Contracts.Generic.QueryStrings;
using EduFlowHub.Api.Contracts.Generic.Responses;
using EduFlowHub.Api.Contracts.LessonMaterial.Requests;
using EduFlowHub.Api.Contracts.LessonMaterial.Responses;
using EduFlowHub.Application.Features.Dtos;
using EduFlowHub.Application.Features.LessonMaterial.Commands.ChangeFile;
using EduFlowHub.Application.Features.LessonMaterial.Commands.Create;
using EduFlowHub.Application.Features.LessonMaterial.Commands.Delete;
using EduFlowHub.Application.Features.LessonMaterial.Commands.Update;
using EduFlowHub.Application.Features.LessonMaterial.Dtos;
using EduFlowHub.Application.Features.LessonMaterial.Queries.GetById;
using EduFlowHub.Application.Features.LessonMaterial.Queries.GetByOrderInCourse;
using EduFlowHub.Application.Features.LessonMaterial.Queries.GetFile;
using EduFlowHub.Application.Features.LessonMaterial.Queries.ListOfCourse;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduFlowHub.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/courses/{courseId:guid}/lesson-materials")]
public class LessonMaterialController
    : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    
    public  LessonMaterialController(
        IMediator mediator,
        IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResponse<LessonMaterialResponse>>> Get(
        CancellationToken cancellationToken,
        [FromRoute] Guid courseId,
        [FromQuery] PageQueryString pageQueryString,
        [FromQuery] bool orderByNumber = false)
    {
        var query = new ListLessonMaterialsOfCourseQuery(courseId, orderByNumber, pageQueryString.Page, pageQueryString.PageSize);
        PagedDto<LessonMaterialDto> result = await _mediator.Send(query, cancellationToken);
        var items = _mapper.Map<IEnumerable<LessonMaterialResponse>>(result.Items);
        var response = new PagedResponse<LessonMaterialResponse>(items, result.Page,result.PageSize, result.TotalCount);
        return Ok(response);
    }
    
    [HttpGet("{lessonMaterialId:guid}")]
    public async Task<ActionResult<LessonMaterialResponse>> GetById(
        [FromRoute] Guid courseId,
        [FromRoute] Guid lessonMaterialId,
        CancellationToken cancellationToken)
    {
        var query = new GetLessonMaterialByIdQuery(lessonMaterialId, courseId);
        LessonMaterialDto result = await _mediator.Send(query, cancellationToken);
        var response = _mapper.Map<LessonMaterialResponse>(result);
        return Ok(response);
    }
    
    [HttpGet("by-order/{orderNumber:int}")]
    public async Task<ActionResult<LessonMaterialResponse>> GetByOrderNumber(
        [FromRoute] Guid courseId,
        [FromRoute] int orderNumber,
        CancellationToken cancellationToken)
    {
        var query = new GetLessonMaterialByOrderInCourseQuery(
            courseId,
            orderNumber);
        LessonMaterialDto result = await _mediator.Send(query, cancellationToken);
        var response = _mapper.Map<LessonMaterialResponse>(result);
        return Ok(response);
    }


    [Authorize(Roles = "Admin, Teacher")]
    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult<LessonMaterialResponse>> Create(
        [FromRoute] Guid courseId,
        [FromForm] CreateLessonMaterialRequest request,
        CancellationToken cancellationToken)
    {
        await using Stream fileStream = request.File.OpenReadStream();
        string fileExtension = Path.GetExtension(request.File.FileName);
        long fileLength = request.File.Length;
        var command = new CreateLessonMaterialCommand(
            request.Title,
            request.OrderNumber,
            fileStream,
            fileExtension,
            fileLength,
            courseId);
        LessonMaterialDto result = await _mediator.Send(command, cancellationToken);
        var response = _mapper.Map<LessonMaterialResponse>(result);
        return CreatedAtAction(
            nameof(GetById),
            new { courseId, lessonMaterialId = result.Id } ,
            response);
    }
    
    [Authorize(Roles = "Admin, Teacher")]
    [HttpPatch("{lessonMaterialId:guid}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid courseId,
        [FromRoute] Guid lessonMaterialId,
        [FromBody] UpdateLessonMaterialRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateLessonMaterialCommand(
            lessonMaterialId,
            courseId,
            request.Title,
            request.OrderNumber);
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
    
    [Authorize(Roles = "Admin, Teacher")]
    [HttpDelete("{lessonMaterialId:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid courseId,
        [FromRoute] Guid lessonMaterialId,
        CancellationToken cancellationToken)
    {
        var command = new DeleteLessonMaterialCommand(lessonMaterialId, courseId);
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
    
    [HttpGet("{lessonMaterialId:guid}/file")]
    public async Task<IActionResult> GetFile(
        [FromRoute] Guid courseId,
        [FromRoute] Guid lessonMaterialId,
        CancellationToken cancellationToken)
    {
        var query = new GetLessonMaterialFileQuery(lessonMaterialId, courseId);
        var result = await _mediator.Send(query, cancellationToken);
        return File(
            result.FileStream, 
            ExtensionToContentTypeHelper.GetContentTypeFromExtension(result.FileExtension), 
            result.FileName);
    }
    
    [HttpPut("{lessonMaterialId:guid}/file")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> ChangeFile(
        [FromRoute] Guid courseId,
        [FromRoute] Guid lessonMaterialId,
        [FromForm] IFormFile file,
        CancellationToken cancellationToken)
    {
        await using Stream fileStream = file.OpenReadStream();
        string fileExtension = Path.GetExtension(file.FileName);
        var fileLength = file.Length;
        var command = new ChangeLessonMaterialFileCommand(
            lessonMaterialId,
            courseId,
            fileStream,
            fileExtension,
            fileLength);
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
}