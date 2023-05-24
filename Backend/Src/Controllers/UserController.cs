using Backend.Src.Common;
using Backend.Src.DTOs;
using Backend.Src.Repositories;
using Backend.Src.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Src.Controllers;

[Route("api/v1/Users")]
public class UserController : ApiControllerBase
{
    private readonly IUserService _service;
    private readonly IJwtTokenService _tokenService;

    public UserController(IUserService service, IJwtTokenService tokenService)
    {
        _service = service;
        _tokenService = tokenService;
    }

    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        Request.Headers.TryGetValue("Authorization", out var token);
        var userId = _tokenService.ReadUserIdFromToken(token[0].Replace("Bearer ", string.Empty));
        return Ok(await _service.GetUserProfile(userId));
    }
    
    [HttpGet]
    public async Task<ICollection<UserReadDTO>> GetAll()
    {
        var filter = Request.QueryString.ParseParams<MatchDTO>();
        if (filter == null)
        {
            return await _service.GetAllUsersAsync(new BaseQueryOptions());
        }
        return await _service.GetAllUsersAsync(filter);
    }
}