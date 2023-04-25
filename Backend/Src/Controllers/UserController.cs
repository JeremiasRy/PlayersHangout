namespace Backend.Src.Controllers;

using Backend.Src.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
public class UserController : ApiControllerBase
{
    private readonly IUserService _service;
    
    public UserController(IUserService service) => _service = service;
            
    [HttpGet]
    public async Task<IActionResult> GetProfile()
    {
        return Ok(await _service.GetUserProfile());
    }
}