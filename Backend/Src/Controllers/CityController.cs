using Backend.Src.Common;
using Backend.Src.DTOs;
using Backend.Src.Models;
using Backend.Src.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Src.Controllers;
[Route("/api/v1/cities")]
public class CityController : BaseController<City, CityDTO, CityDTO, CityDTO>
{
    public CityController(ICityService service) : base(service)
    {
    }
    [AllowAnonymous]
    public async override Task<ICollection<CityDTO>> GetAll()
    {
        var filter = Request.QueryString.ParseParams<NameFilter>();
        return await _service.GetAllAsync(filter);
    }
}
