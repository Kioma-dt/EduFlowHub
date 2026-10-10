using EduFlowHub.Api.Contracts.Auth.Requests;
using EduFlowHub.Api.Contracts.Auth.Responses;
using EduFlowHub.Application.Features.Authentication.Commands.Register;
using EduFlowHub.Application.Features.Authentication.Dtos;
using EduFlowHub.Application.Features.Authentication.Queries.Login;
using EduFlowHub.Application.Features.User.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduFlowHub.Api.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController
    : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    
    public  AuthController(
        IMediator mediator,
        IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequest request,
        CancellationToken cancellationToken)
    {
        var command = _mapper.Map<RegisterCommand>(request);
        UserDto result = await _mediator.Send(command, cancellationToken);
        var response =  _mapper.Map<RegisterResponse>(result);
        return StatusCode(StatusCodes.Status201Created, response);
    }
    
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request,
        CancellationToken cancellationToken)
    {
        var query = _mapper.Map<LoginQuery>(request);
        UserTokenDto result = await _mediator.Send(query,  cancellationToken);
        var response =  _mapper.Map<LoginResponse>(result);
        return Ok(response);
    }
}