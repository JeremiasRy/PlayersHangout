namespace Backend.Src.Controllers;

using Backend.Src.Common;
using Backend.Src.DTOs;
using Backend.Src.Repositories;
using Backend.Src.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
public class UserController : ApiControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service) => _service = service;

    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        return Ok(await _service.GetUserProfile());
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