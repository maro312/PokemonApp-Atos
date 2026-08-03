using Application.Contracts;
using Application.Dtos.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class Authentication : ControllerBase
{
    private readonly IAuthenticationAppService _authenticationAppService;

    public Authentication(IAuthenticationAppService authenticationAppService)
    {
        _authenticationAppService = authenticationAppService;
    }

    /// <summary>
    /// Register a new user and generate a JWT token.
    /// </summary>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        var result = await _authenticationAppService.Register(dto);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Errors.FirstOrDefault());
        }

        return Ok(new { token = result.Value });
    }

    /// <summary>
    /// Generate a JWT token to login.
    /// </summary>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login(LoginDto dto) 
    {
        var result = await _authenticationAppService.Login(dto);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Errors.FirstOrDefault());
        }

        return Ok(result);
    }
}
