using Backend.Src.DTOs;
using Backend.Src.Models;
using Backend.Src.Services;

namespace Backend.Src.Controllers;

public class InstrumentController : BaseController<Instrument, InstrumentDTO, InstrumentDTO, InstrumentDTO>
{
    public InstrumentController(IBaseService<Instrument, InstrumentDTO, InstrumentDTO, InstrumentDTO> service) : base(service)
    {
    }
}