using Backend.Src.DTOs;
using Backend.Src.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Src.Controllers;

[Route("api/v1/Auth")]
public class AuthController : ApiControllerBase
{
    private readonly IAuthService _service;
    private readonly IJwtTokenService _tokenService;

    public AuthController(IAuthService service, IJwtTokenService tokenService)
    {
        _service = service;
        _tokenService = tokenService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(AuthSignInDTO request)
    {
        try
        {
            return Ok(await _service.Login(request));
        } catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
    }

    [AllowAnonymous]
    [HttpPost("signup")]
    public async Task<IActionResult> SignUp(AuthSignUpDTO request)
    {
        try
        {
            return Ok(await _service.SignUp(request));
        } catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        if (Request.Headers.TryGetValue("Authorization", out var token))
        {
            var userId = _tokenService.ReadUserIdFromToken(token[0].Replace("Bearer ", string.Empty));
            return Ok(await _service.Logout(userId));
        };
        return BadRequest();   
    }
}