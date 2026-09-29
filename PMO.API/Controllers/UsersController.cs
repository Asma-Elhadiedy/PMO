
using PMO.Application.Features.Users.Commands.Login;
using PMO.Application.Features.Users.Commands.Logout;
using PMO.Application.Features.Users.Commands.Register;

namespace PMO.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[ProducesResponseType(typeof(Result<bool>), StatusCodes.Status400BadRequest)]
public class UsersController(IMediator _mediator, ILogger<UsersController> _logger) : ControllerBase
{
    [HttpPost("Login")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status202Accepted)]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        var response = await _mediator.Send(command);
        if (response.IsSuccess)
            return Accepted(response);
        return BadRequest(response);
    }

    [HttpPost("Register")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status201Created)]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        var response = await _mediator.Send(command);
        if (response.IsSuccess)
            return Created();

        return BadRequest(response);
    }

    [HttpPost("Logout")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Logout()
    {
        var u = User.Identity?.Name;
        await _mediator.Send(new LogoutUserCommand());
        return Ok();
    }
}
