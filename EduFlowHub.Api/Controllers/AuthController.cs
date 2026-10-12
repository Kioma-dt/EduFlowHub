using EduFlowHub.Api.Contracts.Auth.Requests;
using EduFlowHub.Api.Contracts.Auth.Responses;
using EduFlowHub.Application.Features.Authentication.Commands.Login;
using EduFlowHub.Application.Features.Authentication.Commands.Register;
using EduFlowHub.Application.Features.Authentication.Dtos;
using EduFlowHub.Application.Features.User.Dtos;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace EduFlowHub.Api.Controllers;

[ApiController]
[EnableRateLimiting("auth")]
[ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
[ProducesResponseType<ProblemDetails>(StatusCodes.Status413PayloadTooLarge)]
[ProducesResponseType<ProblemDetails>(StatusCodes.Status429TooManyRequests)]
[ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
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
    [ProducesResponseType<RegisterResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status403Forbidden)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
    [HttpPost("register")]
    public async Task<ActionResult<RegisterResponse>> Register(
        [FromBody] RegisterRequest request,
        CancellationToken cancellationToken)
    {
        var command = _mapper.Map<RegisterCommand>(request);
        UserDto result = await _mediator.Send(command, cancellationToken);
        var response =  _mapper.Map<RegisterResponse>(result);
        return Created($"api/v1/users/me", response);
    }
    
    [AllowAnonymous]
    [ProducesResponseType<LoginResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status413PayloadTooLarge)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status429TooManyRequests)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
    [HttpPost("login")]
    
    public async Task<ActionResult<LoginResponse>> Login(
        [FromBody] LoginRequest request,
        CancellationToken cancellationToken)
    {
        var query = _mapper.Map<LoginCommand>(request);
        UserTokenDto result = await _mediator.Send(query,  cancellationToken);
        var response =  _mapper.Map<LoginResponse>(result);
        return Ok(response);
    }
}