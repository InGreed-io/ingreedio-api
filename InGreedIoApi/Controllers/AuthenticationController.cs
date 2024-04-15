using InGreedIoApi.Model;
using InGreedIoApi.Model.Requests;
using InGreedIoApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace InGreedIoApi.Controllers;

[Route("api/[controller]/")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register([FromBody] UserRegistrationRequest requestDto)
    {
        if (!ModelState.IsValid) return BadRequest(new AuthResult
        {
            Errors = [ "Invalid payload" ],
            Result = false
        });

        var result = await _authenticationService.Register(requestDto);
        if (result.Result == false) return BadRequest(result);

        return Ok(result);
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequest loginRequest)
    {
        if (!ModelState.IsValid) return BadRequest(new AuthResult
        {
            Errors = [ "Invalid payload" ],
            Result = false
        });

        var loginResult = await _authenticationService.Login(loginRequest);
        if (loginResult.Result == false) return BadRequest(loginResult);

        return Ok(loginResult);
    }
}