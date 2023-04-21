using Backend.Src.DTOs;
using Backend.Src.Models;
using Backend.Src.Services.BaseService;

namespace Backend.Src.Services.InstrumentService;

public interface IInstrumentService : IBaseService<Instrument, InstrumentDTO, InstrumentDTO, InstrumentDTO>
{
}
