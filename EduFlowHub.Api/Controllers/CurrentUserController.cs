using EduFlowHub.Api.Contracts.CurrentUser.Requests;
using EduFlowHub.Api.Contracts.CurrentUser.Responses;
using EduFlowHub.Application.Abstractions.Services.User;
using EduFlowHub.Application.Features.User.Commands.ChangePassword;
using EduFlowHub.Application.Features.User.Commands.Update;
using EduFlowHub.Application.Features.User.Dtos;
using EduFlowHub.Application.Features.User.Queries.GetById;

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
[Route("api/v1/users/me")]
public class CurrentUserController
    : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;
    
    public  CurrentUserController(
        IMediator mediator,
        IMapper mapper,
        ICurrentUser currentUser)
    {
        _mediator = mediator;
        _mapper = mapper;
        _currentUser = currentUser;
    }
    
    [ProducesResponseType<CurrentUserResponse>(StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<ActionResult<CurrentUserResponse>> GetById(CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(_currentUser.CurrentUserId);
        UserDto result = await _mediator.Send(query, cancellationToken);
        var response =  _mapper.Map<CurrentUserResponse>(result);
        return Ok(response);
    }
    
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
    [HttpPatch]
    public async Task<IActionResult> Update(
        [FromBody] UpdateCurrentUserRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateUserCommand(
            _currentUser.CurrentUserId,
            request.FirstName,
            request.LastName); 
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
    
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpPut("password")]
    public async Task<IActionResult> ChangePassword(
        [FromBody] ChangeCurrentUserPasswordRequest request,
        CancellationToken cancellationToken)
    {
        var command = new ChangeUserPasswordCommand(
            _currentUser.CurrentUserId,
            request.Password,
            request.OldPassword);
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
}