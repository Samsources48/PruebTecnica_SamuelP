using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto request)
    {
        var result = await authService.LoginAsync(request);
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpPost("register")]
    public async Task<ActionResult<RegisterUserResponseDto>> Register([FromBody] RegisterUserRequestDto request)
    {
        var result = await authService.RegisterAsync(request);
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
}
