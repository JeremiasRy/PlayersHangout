using Backend.Src.Common;
using Backend.Src.DTOs;
using Backend.Src.Models;
using Backend.Src.Services;

namespace Backend.Src.Controllers;

public class CityController : BaseController<City, CityDTO, CityDTO, CityDTO>
{
    public CityController(IBaseService<City, CityDTO, CityDTO, CityDTO> service) : base(service)
    {
    }
    public async override Task<ICollection<CityDTO>> GetAll()
    {
        var filter = Request.QueryString.ParseParams<NameFilter>();
        return await _service.GetAllAsync(filter);
    }
}
