namespace Backend.Src.Controllers;

using Backend.Src.Common;
using Backend.Src.DTOs;
using Backend.Src.Models;
using Backend.Src.Services;

public class WantedController : BaseController<Wanted, WantedReadDTO, WantedCreateDTO, WantedUpdateDTO>
{
    public WantedController(IBaseService<Wanted, WantedReadDTO, WantedCreateDTO, WantedUpdateDTO> _service) : base(_service)
    {
    }
    public async override Task<ICollection<WantedReadDTO>> GetAll()
    {
        var filter = Request.QueryString.ParseParams<MatchDTO>();
        return await _service.GetAllAsync(filter);    
    }
}
