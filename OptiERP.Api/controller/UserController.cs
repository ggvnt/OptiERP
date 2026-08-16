using Microsoft.AspNetCore.Mvc;
using OptiERP.Application.UserCommands.UserRegister;

namespace OptiERP.Api.Controller;

[ApiController]
[Route("api/[controller]")]

public class UserController : ControllerBase
{
    private readonly UserRegisterCommandHandler _userRegisterCommandHandler;

    public UserController(UserRegisterCommandHandler handler)
    {
        _userRegisterCommandHandler = handler;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        UserRegisterCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _userRegisterCommandHandler.Handle(command, cancellationToken);

        return result.Match<IActionResult>(
            success => Ok(success),
            errors => BadRequest(errors));
    }

}