using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OptiERP.Application.Interfaces;
using OptiERP.Application.Interfaces.Authentication;
using OptiERP.Application.UserCommands.GetCurrentUser;
using OptiERP.Application.UserCommands.Login.Normal;
using OptiERP.Application.UserCommands.UserRegister;

namespace OptiERP.Api.Controller;

[ApiController]
[Route("api/[controller]")]

public class UserController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMediator _mediator;

    public UserController(ISender sender, IUserRepository userRepository, ICurrentUserService currentUserService, IMediator mediator)
    {
        _sender = sender;
        _userRepository = userRepository;
        _currentUserService = currentUserService;
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        UserRegisterCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);

        return result.Match<IActionResult>(
            success => Ok(success),
            errors => BadRequest(errors));
    }

    [HttpGet("login")]
    public async Task<IActionResult> Login(
        UserLoginCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);

        return result.Match<IActionResult>(
            success => Ok(success),
            errors => BadRequest(errors));
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetMe(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetCurrentUserQuery(), cancellationToken);

        if (result.IsError)
        {
            return Problem(
                statusCode: 401,
                title: "Unauthorized",
                detail: result.FirstError.Description);
        }

        return Ok(result.Value);
    }

    [Authorize]
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserById(Guid userId, CancellationToken cancellationToken)
    {
        var result = await _userRepository.GetUserByIdAsync(userId, cancellationToken);

        if (result.IsError)
        {
            return Problem(
                statusCode: 404,
                detail: result.FirstError.Description);
        }

        return Ok(result.Value);
    }

}