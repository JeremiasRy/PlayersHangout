using Backend.Src.Common;
using Backend.Src.DTOs.Filter;
using Backend.Src.DTOs.Wanted;
using Backend.Src.Models;
using Backend.Src.Services.WantedService;

namespace Backend.Src.Controllers;

public class WantedController : BaseController<Wanted, WantedReadDTO, WantedCreateDTO, WantedUpdateDTO>
{
    public WantedController(IWantedService _service) : base(_service)
    {
    }
    public async override Task<ICollection<WantedReadDTO>> GetAll()
    {
        var filter = Request.QueryString.ParseParams<MatchDTO>();
        return await _service.GetAllAsync(filter);    
    }
}
