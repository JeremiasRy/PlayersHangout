using Backend.Src.Common;
using Backend.Src.DTOs;
using Backend.Src.Models;
using Backend.Src.Services;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Src.Controllers;

public class InstrumentController : BaseController<Instrument, InstrumentDTO, InstrumentDTO, InstrumentDTO>
{
    public InstrumentController(IBaseService<Instrument, InstrumentDTO, InstrumentDTO, InstrumentDTO> service) : base(service)
    {
    }
    [AllowAnonymous]
    public async override Task<ICollection<InstrumentDTO>> GetAll()
    {
        var filter = Request.QueryString.ParseParams<NameFilter>();
        return await _service.GetAllAsync(filter);
    }
}