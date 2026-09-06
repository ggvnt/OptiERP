using MediatR;
using Microsoft.AspNetCore.Mvc;
using OptiERP.Application.Interfaces.Authentication;
using OptiERP.Application.UserCommands.Login.Normal;
using OptiERP.Application.UserCommands.UserRegister;

namespace OptiERP.Api.Controller;

[ApiController]
[Route("api/[controller]")]

public class UserController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public UserController(ISender sender, IUserRepository userRepository)
    {
        _sender = sender;
        _userRepository = userRepository;
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

}