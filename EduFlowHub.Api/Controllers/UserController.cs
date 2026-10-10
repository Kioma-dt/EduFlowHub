using EduFlowHub.Api.Contracts.User.Requests;
using EduFlowHub.Api.Contracts.User.Responses;
using EduFlowHub.Application.Features.User.Commands.ChangeName;
using EduFlowHub.Application.Features.User.Commands.ChangePassword;
using EduFlowHub.Application.Features.User.Commands.ChangeRole;
using EduFlowHub.Application.Features.User.Commands.Create;
using EduFlowHub.Application.Features.User.Commands.Delete;
using EduFlowHub.Application.Features.User.Commands.Update;
using EduFlowHub.Application.Features.User.Dtos;
using EduFlowHub.Application.Features.User.Queries.GetById;
using EduFlowHub.Application.Features.User.Queries.List;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduFlowHub.Api.Controllers;

[ApiController]
[Authorize(Roles = "Admin")]
[Route("api/v1/users")]
public class UserController
    : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    
    public  UserController(
        IMediator mediator,
        IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var query = new ListAllUsersQuery();
        IEnumerable<UserDto> result = await _mediator.Send(query, cancellationToken);
        var response = _mapper.Map<IEnumerable<UserResponse>>(result);
        return Ok(response);
    }
    
    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid userId,
        CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(userId);
        UserDto result = await _mediator.Send(query, cancellationToken);
        var response =  _mapper.Map<UserResponse>(result);
        return Ok(response);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateUserRequest request,
        CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateUserCommand>(request);
        UserDto result = await _mediator.Send(command, cancellationToken);
        var response =  _mapper.Map<UserResponse>(result);
        return CreatedAtAction(
            nameof(GetById), 
            new { userId = response.Id }, 
            response);
    }
    
    [HttpPatch("{userId:guid}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid userId,
        [FromBody] UpdateUserRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateUserCommand(
            userId,
            request.FirstName,
            request.LastName); 
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
    
    [HttpDelete("{userId:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid userId,
        CancellationToken cancellationToken)
    {
        var command = new DeleteUserCommand(userId);
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
    
    [HttpPut("{userId:guid}/name")]
    public async Task<IActionResult> ChangeUserName(
        [FromRoute] Guid userId,
        [FromBody] ChangeUsernameRequest request,
        CancellationToken cancellationToken)
    {
        var command = new ChangeUserNameCommand(
            userId,
            request.Username);
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
    
    [HttpPut("{userId:guid}/password")]
    public async Task<IActionResult> ChangePassword(
        [FromRoute] Guid userId,
        [FromBody] ChangeUserPasswordRequest request,
        CancellationToken cancellationToken)
    {
        var command = new ChangeUserPasswordCommand(
            userId,
            request.Password,
            null);
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
    
    [HttpPut("{userId:guid}/role")]
    public async Task<IActionResult> ChangeRole(
        [FromRoute] Guid userId,
        [FromBody] ChangeUserRoleRequest request,
        CancellationToken cancellationToken)
    {
        var command = new ChangeUserRoleCommand(
            userId,
            request.Role);
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
}