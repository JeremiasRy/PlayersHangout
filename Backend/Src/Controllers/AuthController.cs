namespace Backend.Src.Controllers;

using Backend.Src.DTOs;
using Backend.Src.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
public class AuthController : ApiControllerBase
{
    private readonly IAuthService _service;
    
    public AuthController(IAuthService service) => _service = service;

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(AuthSignInDTO request)
    {
        return Ok(await _service.Login(request));
    }

    [AllowAnonymous]
    [HttpPost("signup")]
    public async Task<IActionResult> SignUp(AuthSignUpDTO request)
    {
        return Ok(await _service.SignUp(request));
    }
            
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        return Ok(await _service.Logout());
    }
}